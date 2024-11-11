using UnityEngine;


/// <summary>
/// Manages the gameplay, start, end, score, etc
/// </summary>
public class GameMgr : Singleton<GameMgr> 
{
    float score;

    bool isGameRunning;

    public bool IsGameRunning
    {
        get { return isGameRunning; }
        set { isGameRunning = value; }
    }

    public float Score { get => score; set => score = value; }

    public override void Awake() {
        base.Awake();
    }


    public void AddScore(float value)
    {
        score += value;
    }

    public void ResetScore()
    {
        score = 0;
    }


    public void SubtractScore(float value)
    {
        score -= value;
    }

    public void GameOver()
    {
        isGameRunning = false;
        AudioMgr.Instance.PauseMusic();
        SceneMgr.Instance.LoadScene(GameScenes.GameOver, GameMenus.GameOverMenu);
    }

    public void NextLevel()
    {
        // Transition to next level
        Debug.Log("Next level!");
    }

    public void StartGame()
    {
        isGameRunning = true;
    }

    public void PauseGame()
    {
        if (isGameRunning)
        {

            AudioMgr.Instance.PauseMusic();
            isGameRunning = false;
            // Open pause menu here
            UIMgr.Instance.ShowMenu(GameMenus.PauseMenu);
            //Debug.Log("Pause state enabled");
        }
        else
        {
            UnpauseGame();
        }
    }

    public void UnpauseGame()
    {
        AudioMgr.Instance.ResumeMusic();
        isGameRunning = true;
        UIMgr.Instance.HideMenu(GameMenus.PauseMenu);
    }



}