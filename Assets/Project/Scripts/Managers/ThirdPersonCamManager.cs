using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamManager : MonoBehaviour {
    [Header("References")] public Transform orientation;
    public Transform player;
    public Transform playerObj;

    public float rotationSpeed;

    private bool _initialized;

    private void Start() {
        GameManager.Instance.SetCameraProperties += SetProperties;
    }


    private void Update() {
        if (!_initialized) return;

        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (inputDir != Vector3.zero) {
            player.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }

    private void SetProperties(Transform orientation, Transform player, Transform playerObj) {
        this.orientation = orientation;
        this.player = player;
        this.playerObj = playerObj;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _initialized = true;
    }
}