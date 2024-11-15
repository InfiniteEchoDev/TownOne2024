using System.Collections;
using UnityEngine;

public class BootloaderMgr : MonoBehaviour
{
    private void Start()
    {
        UIMgr.Instance.ShowSplash(OnSplashAnimationComplete);
    }
    
    private void OnSplashAnimationComplete()
    {
        SceneMgr.Instance.LoadScene(GameScenes.MainMenu, GameMenus.MainMenu);
    }
}
