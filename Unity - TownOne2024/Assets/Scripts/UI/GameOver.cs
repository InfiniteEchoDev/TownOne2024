using UnityEngine;

public class GameOver : MenuBase
{

    public override GameMenus MenuType()
    {
        return GameMenus.GameOverMenu;
    }
    
    private void OnEnable()
    {
        AudioMgr.Instance.PlaySound(AudioMgr.SoundTypes.GameOver, 0.5f);
    }

    public void Retry()
    {
        AudioMgr.Instance.PauseMusic();
        SceneMgr.Instance.LoadScene(GameScenes.SnakeLike, GameMenus.InGameUI);
    }

    public void MainMenu()
    {
        AudioMgr.Instance.PauseMusic();
        SceneMgr.Instance.LoadScene(GameScenes.MainMenu, GameMenus.MainMenu);
    }
}
