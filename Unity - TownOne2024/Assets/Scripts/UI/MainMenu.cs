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
        SceneMgr.Instance.LoadScene(GameScenes.StarFieldLevel, GameMenus.InGameUI);
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
