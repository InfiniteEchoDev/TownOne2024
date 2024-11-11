using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MenuBase
{
    [SerializeField] private Button _continueButton;
    
    public override GameMenus MenuType()
    {
        return GameMenus.PauseMenu;
    }

    private void OnEnable()
    {
        _continueButton.Select();
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
