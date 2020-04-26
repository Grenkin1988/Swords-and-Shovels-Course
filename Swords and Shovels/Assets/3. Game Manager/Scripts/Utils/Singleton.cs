using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T> {
    private static T instance;
    public static T Instance {
        get => instance;
    }

    public static bool IsInitialized => instance != null;

    protected virtual void Awake() {
        if(instance != null){
             Destroy(gameObject);
            Debug.LogError("[Singleton] Trying to instantiate a second instance of a singleton class.", this);
        }else{
            instance = (T)this;
        }
    }

    protected virtual void OnDestroy() {
        if(instance == this){
            instance = null;
        }
    }
}
