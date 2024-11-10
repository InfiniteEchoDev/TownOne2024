using UnityEngine;

public class TutorialMenu : MenuBase
{
    
    public override GameMenus MenuType()
    {
        return GameMenus.TutorialMenu;
    }

    public void PlayGame()
    {
        SceneMgr.Instance.LoadScene(GameScenes.SnakeLike, GameMenus.InGameUI);
    }

    public void Back()
    {
        UIMgr.Instance.HideMenu(GameMenus.TutorialMenu);
        UIMgr.Instance.ShowMenu(GameMenus.ControlMenu);
    }

}
