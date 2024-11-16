using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PauseMenu : MenuBase
{
    [FormerlySerializedAs("_continueButton")] [SerializeField] private Button ContinueButton;
    
    public override GameMenus MenuType()
    {
        return GameMenus.PauseMenu;
    }

    private void OnEnable()
    {
        ContinueButton.Select();
    }

    public void MainMenu()
    {
        SceneMgr.Instance.LoadScene(GameScenes.MainMenu, GameMenus.MainMenu);
    }

    public void Resume()
    {
        GameMgr.Instance.UnpauseGame();
    }
}
