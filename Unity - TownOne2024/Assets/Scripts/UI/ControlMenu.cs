using UnityEngine;

public class ControlMenu : MenuBase
{
    public override GameMenus MenuType()
    {
        return GameMenus.ControlMenu;
    }

    public void NextMenu()
    {
        UIMgr.Instance.HideMenu(GameMenus.ControlMenu);
        UIMgr.Instance.ShowMenu(GameMenus.TutorialMenu);
        //SceneMgr.Instance.LoadScene(GameScenes.StarFieldLevel, GameMenus.InGameUI);
    }

    public void Back()
    {
        UIMgr.Instance.HideMenu(GameMenus.ControlMenu);
        UIMgr.Instance.ShowMenu(GameMenus.MainMenu);
    }
}
