using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TutorialMenu : MenuBase
{
    [FormerlySerializedAs("_nextButton")] [SerializeField] private Button NextButton;

    private void OnEnable()
    {
        NextButton.Select();
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
