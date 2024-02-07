using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager _instance;

    public static GameManager Instance {
        get {
            if (_instance == null) {
                // If the instance is null, try to find an existing GameManager in the scene
                _instance = FindObjectOfType<GameManager>();

                // If no GameManager exists, create a new one
                if (_instance == null) {
                    GameObject singletonObject = new GameObject("GameManager");
                    _instance = singletonObject.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }

    private void Awake() {
        // Ensure there is only one instance of the GameManager
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public Action OnHostButton;
    public Action OnJoinButton;
    public Action CloseButtons;

    public Action<string> SetPlayerName;
}