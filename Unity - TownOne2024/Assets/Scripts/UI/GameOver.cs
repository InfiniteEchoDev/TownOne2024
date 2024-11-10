using UnityEngine;

public class GameOver : MenuBase
{
    [SerializeField] AudioClip audioClip;

    public override GameMenus MenuType()
    {
        return GameMenus.GameOverMenu;
    }
    
    private void OnEnable()
    {
        AudioMgr.Instance.PlayOneShotMusic(audioClip, 0.5f);
    }

    public void Retry()
    {
        AudioMgr.Instance.PauseMusic();
        SceneMgr.Instance.LoadScene(GameScenes.StarFieldLevel, GameMenus.InGameUI);
    }

    public void MainMenu()
    {
        AudioMgr.Instance.PauseMusic();
        SceneMgr.Instance.LoadScene(GameScenes.MainMenu, GameMenus.MainMenu);
    }
}
