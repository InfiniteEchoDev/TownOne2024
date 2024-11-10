using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MenuBase
{
    [SerializeField] private Button _startButton;
    
    public override GameMenus MenuType()
    {
        return GameMenus.MainMenu;
    }

    private void OnEnable()
    {
        _startButton.Select();
    }

    public void StartButton()
    {
        UIMgr.Instance.HideMenu(GameMenus.MainMenu);
        UIMgr.Instance.ShowMenu(GameMenus.ControlMenu);
    }

    public void SettingsButton()
    {
        UIMgr.Instance.ShowMenu(GameMenus.SettingsMenu);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
