using System;
using UnityEngine;

public class UIManager : Singleton<UIManager> {
    [SerializeField]
    private MainMenu _mainMenu;
    [SerializeField]
    private PauseMenu _pauseMenu;
    [SerializeField]
    private Camera _dummyCamera;

    private void Start() {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameState current, GameState previous) {
        _pauseMenu.gameObject.SetActive(current == GameState.PAUSED);
    }

    private void Update() {
        if(GameManager.Instance.CurrentGameState != GameState.PREGAME) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            GameManager.Instance.StartGame();
        }
    }

    public void SetDummyCameraActive(bool active) {
        _dummyCamera.gameObject.SetActive(active);
    }
}
