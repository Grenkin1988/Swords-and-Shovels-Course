﻿using UnityEngine;

public class MainMenu : MonoBehaviour {
    [SerializeField]
    private Animation _mainMenuAnimation;
    [SerializeField]
    private AnimationClip _fadeOutAnimation;
    [SerializeField]
    private AnimationClip _fadeInAnimation;

    public void OnFadeOutComplete() {
        Debug.Log(nameof(OnFadeOutComplete));
    }

    public void OnFadeInComplete() {
        Debug.Log(nameof(OnFadeInComplete));
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
