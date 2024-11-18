using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class GameOver : MenuBase
{
    [FormerlySerializedAs("playerScore")] [SerializeField]
    TMP_Text PlayerScore;
    [FormerlySerializedAs("highScore")] [SerializeField]
    TMP_Text HighScore;
    GameMgr _gameMgr;
    GameLoopManager _gameLoopManager;

    [FormerlySerializedAs("retry")] [SerializeField]
    Button Retry;

    int _score;
    
    void Start()
    {
        Retry.Select();
        int intScore = (int)Mathf.Round(GameMgr.Instance.Score); 
        PlayerScore.text = intScore.ToString();
        if (SaveUtil.SavedValues?.HighScore == null)
        {
            Debug.LogError("[GameOver] SaveUtil.SavedValues is null");
            return;
        }
        
        if (intScore < SaveUtil.SavedValues.HighScore)
        {
            HighScore.text = SaveUtil.SavedValues.HighScore.ToString();
        }
        else
        {
            HighScore.text = intScore.ToString();
            SaveUtil.SavedValues.HighScore = intScore;
            SaveUtil.Save();
        }
    }
    public override GameMenus MenuType()
    {
        return GameMenus.GameOverMenu;
    }

    public void ButtonRetry()
    {
        _gameMgr.ResetScore();
        _gameLoopManager.ResetLives();

        SceneMgr.Instance.LoadScene(GameScenes.SnakeLike, GameMenus.InGameUI);
    }

    public void ButtonMainMenu()
    {
        _gameMgr.ResetScore();
        _gameLoopManager.ResetLives();
        SceneMgr.Instance.LoadScene(GameScenes.MainMenu, GameMenus.MainMenu);
    }
}
