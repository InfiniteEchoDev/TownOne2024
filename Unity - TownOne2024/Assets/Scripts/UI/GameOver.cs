using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
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
        try
        {
            if (SaveUtil.SavedValues.Score != null && intScore < SaveUtil.SavedValues.Score)
            {
                highScore.text = SaveUtil.SavedValues.Score.ToString();

            }
            else
            {
                highScore.text = intScore.ToString();
                SaveUtil.SavedValues.Score = intScore;
                SaveUtil.Save();
            }
        }
        catch (Exception ex)
        {
            highScore.text = intScore.ToString();
            SaveUtil.SavedValues.Score = intScore;
            SaveUtil.Save();
        }
        /*
        if (SaveUtil.SavedValues.Score != null && intScore < SaveUtil.SavedValues.Score)
        {
            highScore.text = SaveUtil.SavedValues.Score.ToString();

        }
        else
        {
            highScore.text = intScore.ToString();
            SaveUtil.SavedValues.Score = intScore;
            SaveUtil.Save();
        }
        */
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
