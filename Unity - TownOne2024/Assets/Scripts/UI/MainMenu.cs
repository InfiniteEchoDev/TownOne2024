using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenu : MenuBase
{
    [FormerlySerializedAs("_startButton")] [SerializeField] private Button StartGameButton;
    
    public override GameMenus MenuType()
    {
        return GameMenus.MainMenu;
    }

    private void OnEnable()
    {
        StartGameButton.Select();
        AudioMgr.Instance.PlayMusic(AudioMgr.MusicTypes.MainMenu, 0.5f);
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
