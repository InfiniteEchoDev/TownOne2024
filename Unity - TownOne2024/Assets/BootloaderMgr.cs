using System.Collections;
using UnityEngine;

public class BootloaderMgr : MonoBehaviour
{
    private void Start()
    {
        UIMgr.Instance.ShowSplash(OnSplashAnimationComplete);
        //StartCoroutine(ShowSplash());
    }

    private IEnumerator ShowSplash()
    {
        yield return null;
    }

    private void OnSplashAnimationComplete()
    {
        SceneMgr.Instance.LoadScene(GameScenes.MainMenu, GameMenus.MainMenu);
    }
}
