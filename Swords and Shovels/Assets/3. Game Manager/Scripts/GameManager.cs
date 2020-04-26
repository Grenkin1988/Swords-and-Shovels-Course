using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    // what level the game is currently in
    // load and inload levels
    // keep track of the game state
    // ganerate other persistent systems

    private string _currentLevelName = string.Empty;

    private void Start() {
        LoadLevel("Main");
    }

    public void LoadLevel(string levelName) {
        var ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        ao.completed += OnLoadOperationComplete;
        _currentLevelName = levelName;
    }

    private void OnLoadOperationComplete(AsyncOperation ao) {
        Debug.Log("Load Completed.");
    }

    public void UnloadLevel(string levelName) {
        var ao = SceneManager.UnloadSceneAsync(levelName);
        ao.completed += OnUnloadOperationComplete;
    }

    private void OnUnloadOperationComplete(AsyncOperation obj) {
        Debug.Log("Unload Completed.");
    }
}
