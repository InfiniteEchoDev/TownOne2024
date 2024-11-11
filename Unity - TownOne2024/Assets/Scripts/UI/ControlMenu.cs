using UnityEngine;
using UnityEngine.UI;
public class ControlMenu : MenuBase
{
    [SerializeField]
    Button nextButton;
    public override GameMenus MenuType()
    {
        return GameMenus.ControlMenu;
    }

    private void Start()
    {
        nextButton.Select();
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
