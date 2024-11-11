using System;
using UnityEngine;

public class SplashMenu : MenuBase
{
    [SerializeField] private Animator _animator;
    
    private Action _onAnimationComplete;

    [SerializeField] AudioClip audioClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        AudioMgr.Instance.PlayOneShotMusic(audioClip, 0.25f);
    }

    public void OnShow(Action onAnimationComplete)
    {
        _onAnimationComplete = onAnimationComplete;
        _animator.Play(0);
    }

    /// <summary>
    /// Called by the animation event on the splash animation controller
    /// </summary>
    public void AnimationComplete()
    {
        _onAnimationComplete?.Invoke();
    }
}
