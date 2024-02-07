using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] private Button _hostButton, _joinButton;

    private void Start() {
        GameManager.Instance.CloseButtons += CloseButtons;
    }

    public void OnHostButton() {
        GameManager.Instance.OnHostButton.Invoke();
    }

    public void OnJoinButton() {
        GameManager.Instance.OnJoinButton.Invoke();
    }

    private void CloseButtons() {
        _hostButton.gameObject.SetActive(false);
        _joinButton.gameObject.SetActive(false);
    }
}