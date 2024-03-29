using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MultiplayerManager : MonoBehaviour, INetworkRunnerCallbacks {
    [SerializeField] private NetworkPrefabRef _playerPrefab;
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

    private void Start() {
        GameManager.Instance.OnHostButton += OnHostButton;
        GameManager.Instance.OnJoinButton += OnJoinButton;
    }

    #region INTERFACE_METHODS

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) {
        if (runner.IsServer) {
            GameManager.Instance.CloseButtons.Invoke();
            Vector3 spawnPos = new Vector3((player.RawEncoded % runner.Config.Simulation.DefaultPlayers) * 3, 1, 0);
            NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, spawnPos, Quaternion.identity, player);
            _spawnedCharacters.Add(player, networkPlayerObject);
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) {
        if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject)) {
            runner.Despawn(networkObject);
            _spawnedCharacters.Remove(player);
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input) {
        var data = new NetworkInputData();

        Vector2 _moveInputVector = Vector3.zero;

        _moveInputVector.x = Input.GetAxis("Horizontal");
        _moveInputVector.y = Input.GetAxis("Vertical");

        data.MovementInput = _moveInputVector;

        input.Set(data);
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) {
    }

    public void OnConnectedToServer(NetworkRunner runner) {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner) {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) {
    }

    public void OnSceneLoadDone(NetworkRunner runner) {
    }

    public void OnSceneLoadStart(NetworkRunner runner) {
    }

    #endregion

    private NetworkRunner _runner;

    async void StartGame(GameMode gameMode) {
        if (_runner == null) {
            _runner = gameObject.AddComponent<NetworkRunner>();
        }
        else {
            _runner = GetComponent<NetworkRunner>();
        }

        _runner.ProvideInput = true;

        await _runner.StartGame(new StartGameArgs() {
            GameMode = gameMode,
            SessionName = "Teriun Games Room",
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
        });
    }

    private void OnHostButton() {
        StartGame(GameMode.Host);
    }

    private void OnJoinButton() {
        StartGame(GameMode.Client);
    }

    private void OnDestroy() {
        /*GameManager.Instance.OnHostButton -= OnHostButton;
        GameManager.Instance.OnJoinButton -= OnJoinButton;*/
    }
}