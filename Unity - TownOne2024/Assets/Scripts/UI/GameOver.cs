using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameOver : MenuBase
{
    [SerializeField]
    TMP_Text playerScore;
    [SerializeField]
    TMP_Text highScore;
    GameMgr gameMgr;

    [SerializeField]
    Button retry;

    int score;
    void Start()
    {

        int intScore = (int)Mathf.Round(GameMgr.Instance.Score); 
        SaveUtil.Load();
        playerScore.text = intScore.ToString();

        if (intScore > SaveUtil.SavedValues.Score)
        {
            highScore.text = intScore.ToString();
            SaveUtil.SavedValues.Score = intScore;
            SaveUtil.Save();
        }
        else
        {
            highScore.text = SaveUtil.SavedValues.Score.ToString();
        }
        retry.Select();
    }
    public override GameMenus MenuType()
    {
        return GameMenus.GameOverMenu;
    }

    public void Retry()
    {
        SceneMgr.Instance.LoadScene(GameScenes.SnakeLike, GameMenus.InGameUI);
    }

    public void MainMenu()
    {
        SceneMgr.Instance.LoadScene(GameScenes.MainMenu, GameMenus.MainMenu);
    }
}
