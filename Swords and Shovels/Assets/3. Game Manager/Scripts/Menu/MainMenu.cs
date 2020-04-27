using UnityEngine;

public class MainMenu : MonoBehaviour {
    [SerializeField]
    private Animation _mainMenuAnimation;
    [SerializeField]
    private AnimationClip _fadeOutAnimation;
    [SerializeField]
    private AnimationClip _fadeInAnimation;

    public Events.EventFadeComplete OnMainMenuFadeComplete;

    private void Start() {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameState current, GameState previous) {
        if(previous == GameState.PREGAME && current == GameState.RUNNING) {
            FadeOut();
        }

        if (previous != GameState.PREGAME && current == GameState.PREGAME) {
            FadeIn();
        }
    }

    public void OnFadeOutComplete() {
        OnMainMenuFadeComplete?.Invoke(true);
    }

    public void OnFadeInComplete() {
        OnMainMenuFadeComplete?.Invoke(false);
        UIManager.Instance.SetDummyCameraActive(true);
    }

    public void FadeIn() {
        _mainMenuAnimation.Stop();
        _mainMenuAnimation.clip = _fadeInAnimation;
        _mainMenuAnimation.Play();
    }

    public void FadeOut() {
        UIManager.Instance.SetDummyCameraActive(false);

        _mainMenuAnimation.Stop();
        _mainMenuAnimation.clip = _fadeOutAnimation;
        _mainMenuAnimation.Play();
    }
}
