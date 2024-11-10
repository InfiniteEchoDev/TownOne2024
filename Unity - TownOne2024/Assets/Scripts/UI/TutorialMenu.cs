using UnityEngine;

public class TutorialMenu : MenuBase
{
    
    public override GameMenus MenuType()
    {
        return GameMenus.TutorialMenu;
    }

    public void PlayGame()
    {
        SceneMgr.Instance.LoadScene(GameScenes.StarFieldLevel, GameMenus.InGameUI);
    }

}
