using System;
using UnityEngine.Events;

public class Events {
    [Serializable]
    public class EventFadeComplete : UnityEvent<bool> { }
    [Serializable]
    public class EventGameState : UnityEvent<GameState, GameState> { }
}
