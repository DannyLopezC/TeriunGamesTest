using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour {
    private Vector2 _moveInputVector = Vector3.zero;

    private void Update() {
        _moveInputVector.x = Input.GetAxis("Horizontal");
        _moveInputVector.y = Input.GetAxis("Vertical");
    }

    public NetworkInputData GetNetworkInput() {
        NetworkInputData networkInputData = new NetworkInputData();

        networkInputData.MovementInput = _moveInputVector;

        return networkInputData;
    }
}