using UnityEngine;
using UnityEngine.UI;

public class Settings : MenuBase
{
    [SerializeField] private Button BackButton;

    private void OnEnable()
    {
        BackButton.Select();
    }

    public override GameMenus MenuType()
    {
        return GameMenus.SettingsMenu;
    }

    public void Close()
    {
        UIMgr.Instance.HideMenu(GameMenus.SettingsMenu);
    }
}
