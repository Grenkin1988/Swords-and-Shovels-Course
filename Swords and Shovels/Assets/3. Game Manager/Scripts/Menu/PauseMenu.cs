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
    }

    private void HandleResumeClicked() {
        GameManager.Instance.TogglePause();
    }

    private void Update() {

    }
}
