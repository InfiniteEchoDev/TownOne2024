using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class ControlMenu : MenuBase
{
    [FormerlySerializedAs("nextButton")] [SerializeField]
    Button NextButton;
    
    public override GameMenus MenuType()
    {
        return GameMenus.ControlMenu;
    }

    private void Start()
    {
        NextButton.Select();
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
