using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    [SerializeField]
    private Button _resumeButton;
    [SerializeField]
    private Button _restartButton;
    [SerializeField]
    private Button _quitButton;

    private void Start() {
        _resumeButton.onClick.AddListener(HandleResumeClicked);
        _restartButton.onClick.AddListener(HandleRestartClicked);
        _quitButton.onClick.AddListener(HandleQuitClicked);
    }

    private void HandleResumeClicked() {
        GameManager.Instance.TogglePause();
    }

    private void HandleRestartClicked() {
        GameManager.Instance.RestartGame();
    }

    private void HandleQuitClicked() {
        GameManager.Instance.QuitGame();
    }
}
