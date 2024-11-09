using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class GameMgr : Singleton<GameMgr> {


    public override void Awake() {
        base.Awake();
    }

    void Start() {
        BootstrapMainMenu();
    }

    void BootstrapMainMenu() {
        SceneMgr.Instance.LoadAllMetaScenes();

        StartCoroutine( CoroutineUtilities.WaitAFrameAndExecute( () => {
            // Manage newly loaded scenes here
            UIMgr.Instance.ShowMainMenu();
        } ) );
    }


}