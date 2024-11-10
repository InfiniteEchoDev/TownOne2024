using UnityEngine;

public class PauseMenu : MenuBase
{
    public override GameMenus MenuType()
    {
        return GameMenus.PauseMenu;
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
