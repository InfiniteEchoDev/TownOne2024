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
        isGameRunning = false;
    }

}