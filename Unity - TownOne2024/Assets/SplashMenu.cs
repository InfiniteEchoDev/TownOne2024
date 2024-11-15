using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SplashMenu : MenuBase
{
    [FormerlySerializedAs("_animator")] [SerializeField] private Animator Animator;
    
    private Action _onAnimationComplete;

    [FormerlySerializedAs("audioClip")] [SerializeField] AudioClip AudioClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        AudioMgr.Instance.PlayOneShotMusic(AudioClip, 0.25f);
    }

    public void OnShow(Action onAnimationComplete)
    {
        _onAnimationComplete = onAnimationComplete;
        Animator.Play(0);
    }

    /// <summary>
    /// Called by the animation event on the splash animation controller
    /// </summary>
    public void AnimationComplete()
    {
        _onAnimationComplete?.Invoke();
    }
}
