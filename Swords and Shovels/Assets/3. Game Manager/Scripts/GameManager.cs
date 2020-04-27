using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GameState {
    PREGAME,
    RUNNING,
    PAUSED
}

public class GameManager : Singleton<GameManager> {
    

    // keep track of the game state
    [SerializeField]
    private GameObject[] _systemPrefabs;

    private List<GameObject> _instancedSystems;
    private List<AsyncOperation> _loadOperations;

    private string _currentLevelName = string.Empty;

    public Events.EventGameState OnGameStateChanged;

    public GameState CurrentGameState { get; private set; }

    private void Start() {
        DontDestroyOnLoad(gameObject);

        _instancedSystems = new List<GameObject>();
        _loadOperations = new List<AsyncOperation>();

        InstantiateSystemPrefabs();

        UIManager.Instance.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
    }

    private void HandleMainMenuFadeComplete(bool fadeOut) {
        if (!fadeOut) {
            UnloadLevel(_currentLevelName);
        }
    }

    private void Update() {
        if (CurrentGameState == GameState.PREGAME) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }

    private void InstantiateSystemPrefabs() {
        for (int i = 0; i < _systemPrefabs.Length; i++) {
            var systemInstance = Instantiate(_systemPrefabs[i]);
            _instancedSystems.Add(systemInstance);
        }
    }

    public void LoadLevel(string levelName) {
        var ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ao == null) {
            Debug.LogError("[GameManager] Unable to load level.", this);
            return;
        }
        ao.completed += OnLoadOperationComplete;
        _loadOperations.Add(ao);
        _currentLevelName = levelName;
    }

    private void OnLoadOperationComplete(AsyncOperation ao) {
        if (_loadOperations.Contains(ao)) {
            _loadOperations.Remove(ao);

            if (_loadOperations.Count == 0) { 
                UpdateState(GameState.RUNNING); 
            }
        }
        Debug.Log("Load Completed.");
    }

    public void UnloadLevel(string levelName) {
        var ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null) {
            Debug.LogError("[GameManager] Unable to unload level.", this);
            return;
        }
        ao.completed += OnUnloadOperationComplete;
    }

    private void OnUnloadOperationComplete(AsyncOperation obj) {
        Debug.Log("Unload Completed.");
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        for (int i = 0; i < _instancedSystems.Count; i++) {
            Destroy(_instancedSystems[i]);
        }
        _instancedSystems.Clear();
    }

    private void UpdateState(GameState state) {
        var previousGameState = CurrentGameState;
        CurrentGameState = state;

        switch (CurrentGameState) {
            case GameState.PREGAME:
                Time.timeScale = 1.0f;
                break;
            case GameState.RUNNING:
                Time.timeScale = 1.0f;
                break;
            case GameState.PAUSED:
                Time.timeScale = 0.0f;
                break;
            default:
                break;
        }

        OnGameStateChanged?.Invoke(CurrentGameState, previousGameState);
    }

    public void StartGame() {
        LoadLevel("Main");
    }

    public void TogglePause() {
        var newState = 
            CurrentGameState == GameState.RUNNING 
            ? GameState.PAUSED 
            : GameState.RUNNING;
        UpdateState(newState);
    }

    public void RestartGame() {
        UpdateState(GameState.PREGAME);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
