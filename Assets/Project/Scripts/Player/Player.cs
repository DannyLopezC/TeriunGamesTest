using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;

public class Player : NetworkBehaviour {
    private NetworkCharacterControllerPrototype _networkCharacterControllerPrototype;

    [SerializeField] private Transform orientation;
    [SerializeField] private Transform body;

    [SerializeField] private float speed = 6;

    private string _playerName;

    [SerializeField] private TMP_Text _playerNameText;

    private void Awake() {
        _networkCharacterControllerPrototype = GetComponent<NetworkCharacterControllerPrototype>();
    }

    private void Start() {
        GameManager.Instance.SetPlayerName += SetName;
        GameManager.Instance.SetCameraProperties.Invoke(orientation, transform, body);
    }

    public override void FixedUpdateNetwork() {
        if (GetInput(out NetworkInputData data)) {
            Vector3 moveDirection =
                orientation.forward * data.MovementInput.y + orientation.right * data.MovementInput.x;

            _networkCharacterControllerPrototype.Move(moveDirection.normalized * speed * 10f);
        }
    }

    private void SetName(string playerName) {
        _playerName = playerName;

        _playerNameText.text = _playerName;
    }
}