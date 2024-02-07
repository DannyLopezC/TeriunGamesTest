using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;

public class Player : NetworkBehaviour {
    [SerializeField] private float playerHeight;
    private bool grounded;
    [SerializeField] float groundDrag;
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform orientation;

    [SerializeField] private float speed = 6;

    private CharacterController _characterController;

    private string _playerName;

    [SerializeField] private TMP_Text _playerNameText;

    private void Awake() {
        _characterController = GetComponent<CharacterController>();
    }

    private void Start() {
        GameManager.Instance.SetPlayerName += SetName;
    }

    private void Update() {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (grounded) {
            rb.drag = groundDrag;
        }
        else {
            rb.drag = 0;
        }
    }

    public override void FixedUpdateNetwork() {
        if (GetInput(out NetworkInputData data)) {
            Debug.Log($"data {data.MovementInput}");

            Vector3 moveDirection =
                orientation.forward * data.MovementInput.y + orientation.right * data.MovementInput.x;

            rb.AddForce(moveDirection.normalized * (speed * 10f),
                ForceMode.Force);
        }
    }

    private void SetName(string playerName) {
        _playerName = playerName;

        _playerNameText.text = _playerName;
    }
}