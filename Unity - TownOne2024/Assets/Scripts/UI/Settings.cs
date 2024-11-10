using UnityEngine;

public class Settings : MenuBase
{
    public override GameMenus MenuType()
    {
        return GameMenus.SettingsMenu;
    }

    public void MainMenu()
    {
        UIMgr.Instance.ShowMenu(GameMenus.MainMenu);
    }
}
