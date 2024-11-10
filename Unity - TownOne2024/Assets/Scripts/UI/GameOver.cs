using UnityEngine;

public class GameOver : MenuBase
{
    public override GameMenus MenuType()
    {
        return GameMenus.GameOverMenu;
    }

    public void Retry()
    {
        SceneMgr.Instance.LoadScene(GameScenes.MainGame, GameMenus.InGameUI);
    }

    public void MainMenu()
    {
        SceneMgr.Instance.LoadScene(GameScenes.MainMenu, GameMenus.MainMenu);
    }
}
