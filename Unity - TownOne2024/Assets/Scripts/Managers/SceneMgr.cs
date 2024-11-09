using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : Singleton<SceneMgr>
{
    public void LoadScene(GameScenes sceneToLoad, GameMenus menuToOpen)
    {
        StartCoroutine(PerformLoadSequence(sceneToLoad, menuToOpen));
    }

    private IEnumerator PerformLoadSequence(GameScenes sceneToLoad, GameMenus menuToOpen)
    {
        bool waiting = true;

        UIMgr.Instance.CloseAllMenus();

        UIMgr.Instance.ShowMenu(GameMenus.Fader, () => waiting = false);
        
        yield return new WaitWhile(() => waiting);

        var asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad.ToString());

        while (asyncOperation is {isDone: false}) yield return null;
        
        UIMgr.Instance.HideMenu(GameMenus.Fader);
        
        UIMgr.Instance.ShowMenu(menuToOpen);

    }
}
