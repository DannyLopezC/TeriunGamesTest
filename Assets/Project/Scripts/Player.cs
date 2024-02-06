using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;

public class Player : NetworkBehaviour {
    private string _playerName;
    private NetworkCharacterControllerPrototype _characterController;

    [SerializeField] private TMP_Text _playerNameText;

    private void Awake() {
        _characterController = GetComponent<NetworkCharacterControllerPrototype>();
    }

    private void Start() {
        GameManager.Instance.SetPlayerName += SetName;
    }

    public override void FixedUpdateNetwork() {
        if (GetInput(out NetworkInputData data)) {
            Vector3 moveDirection = transform.forward * data.MovementInput.y + transform.right * data.MovementInput.x;
            moveDirection.Normalize();
            _characterController.Move(moveDirection);
        }
    }

    private void SetName(string playerName) {
        _playerName = playerName;

        _playerNameText.text = _playerName;
    }
}