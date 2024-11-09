using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class UIMgr : Singleton<UIMgr> {
    
    [SerializeField]
    private Canvas _mainMenu;

    public override void Awake() {
        base.Awake();
    }


    public void ShowMainMenu() {
        _mainMenu.gameObject.SetActive( true );
    }
}