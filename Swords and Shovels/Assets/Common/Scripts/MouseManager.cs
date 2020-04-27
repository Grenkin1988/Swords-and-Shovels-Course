using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour {
    public LayerMask clickableLayer;

    public Texture2D pointer;
    public Texture2D target;
    public Texture2D doorway;

    public EventVector3 OnClickEnvironment;

    private bool _useDefaultCursor = false;

    private void Start() {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameState current, GameState previous) {
        _useDefaultCursor = current == GameState.PAUSED;
    }

    private void Update() {
        if (_useDefaultCursor) {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
            return;
        }
        // Raycast into scene
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit, 50, clickableLayer.value)) {
            bool door = false;
            if (hit.collider.gameObject.tag == "Doorway") {
                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                door = true;
            } else {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
            }

            // If environment surface is clicked, invoke callbacks.
            if (Input.GetMouseButtonDown(0)) {
                if (door) {
                    var doorway = hit.collider.gameObject.transform;
                    OnClickEnvironment.Invoke(doorway.position + doorway.forward * 10);
                } else {
                    OnClickEnvironment.Invoke(hit.point);
                }
            }
        } else {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }
    }
}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }
