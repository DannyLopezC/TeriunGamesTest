using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public void OnHostButton() {
        GameManager.Instance.OnHostButton.Invoke();
    }

    public void OnJoinButton() {
        GameManager.Instance.OnJoinButton.Invoke();
    }
}