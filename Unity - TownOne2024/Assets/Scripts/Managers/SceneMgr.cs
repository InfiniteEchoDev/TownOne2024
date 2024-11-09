using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : Singleton<SceneMgr>
{
    public void LoadScene(GameScenes sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad.ToString());
    }

    public void LoadScene(string sceneName, GameMenus menuToOpen)
    {
        StartCoroutine(PerformLoadSequence(sceneName, menuToOpen));
    }

    private IEnumerator PerformLoadSequence(string sceneName, GameMenus menuToOpen)
    {
        bool waiting = true;

        UIMgr.Instance.CloseAllMenus();

        UIMgr.Instance.ShowMenu(GameMenus.Fader, () => waiting = false);
        
        yield return new WaitWhile(() => waiting);

        var asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (asyncOperation is {isDone: false}) yield return null;
        
        UIMgr.Instance.ShowMenu(menuToOpen);

        UIMgr.Instance.HideMenu(GameMenus.Fader);

    }
}
