using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMenu : MenuBase
{
    [SerializeField] private Button _nextButton;

    private void OnEnable()
    {
        _nextButton.Select();
    }

    public override GameMenus MenuType()
    {
        return GameMenus.TutorialMenu;
    }

    public void PlayGame()
    {
        AudioMgr.Instance.PauseMusic();
        SceneMgr.Instance.LoadScene(GameScenes.SnakeLike, GameMenus.None);
    }

    public void Back()
    {
        UIMgr.Instance.HideMenu(GameMenus.TutorialMenu);
        UIMgr.Instance.ShowMenu(GameMenus.ControlMenu);
    }

}
