using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneMgr : Singleton<SceneMgr> {
    public string GameMgrSceneName = "GameMgr";
    public Scene GameMgrScene;
    public string UISceneName = "UI";
    public Scene UIScene;
    public string LevelSceneName = "Level";
    public Scene LevelScene;

    public override void Awake() {
        base.Awake();

        if( GameMgrScene.name == null )
            GameMgrScene = SceneManager.GetSceneByName( GameMgrSceneName );
        if( UIScene.name == null )
            UIScene = SceneManager.GetSceneByName( UISceneName );
        if( LevelScene.name == null )
            LevelScene = SceneManager.GetSceneByName( LevelSceneName );
    }


    public void LoadAllMetaScenes() {
        if( !UIScene.isLoaded ) {
            SceneManager.LoadScene( UISceneName, LoadSceneMode.Additive );
        }
        if( !LevelScene.isLoaded ) {
            SceneManager.LoadScene( LevelSceneName, LoadSceneMode.Additive );
        }
    }
}
