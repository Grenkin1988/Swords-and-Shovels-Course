using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
    // keep track of the game state
    // ganerate other persistent systems
    private string _currentLevelName = string.Empty;

    private List<AsyncOperation> _loadOperations;

    private void Start() {
        DontDestroyOnLoad(gameObject);

        _loadOperations = new List<AsyncOperation>();
        LoadLevel("Main");
    }

    public void LoadLevel(string levelName) {
        var ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if(ao == null){
            Debug.LogError("[GameManager] Unable to load level.", this);
            return;
        }
        ao.completed += OnLoadOperationComplete;
        _loadOperations.Add(ao);
        _currentLevelName = levelName;
    }

    private void OnLoadOperationComplete(AsyncOperation ao) {
        if(_loadOperations.Contains(ao)){
            _loadOperations.Remove(ao);
        }
        Debug.Log("Load Completed.");
    }

    public void UnloadLevel(string levelName) {
        var ao = SceneManager.UnloadSceneAsync(levelName);
        if(ao == null){
            Debug.LogError("[GameManager] Unable to unload level.", this);
            return;
        }
        ao.completed += OnUnloadOperationComplete;
    }

    private void OnUnloadOperationComplete(AsyncOperation obj) {
        Debug.Log("Unload Completed.");
    }
}
