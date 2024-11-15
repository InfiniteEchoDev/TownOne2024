using UnityEngine;


/// <summary>
/// Manages the gameplay, start, end, score, etc
/// </summary>
public class GameMgr : Singleton<GameMgr> 
{
    float _score;

    bool _isGameRunning;

    public bool IsGameRunning
    {
        get { return _isGameRunning; }
        set { _isGameRunning = value; }
    }

    public float Score { get => _score; set => _score = value; }

    public override void Awake() {
        base.Awake();
    }


    public void AddScore(float value)
    {
        _score += value;
    }

    public void ResetScore()
    {
        _score = 0;
    }


    public void SubtractScore(float value)
    {
        _score -= value;
    }

    public void GameOver()
    {
        _isGameRunning = false;
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
        _isGameRunning = true;
    }

    public void PauseGame()
    {
        if (_isGameRunning)
        {

            AudioMgr.Instance.PauseMusic();
            _isGameRunning = false;
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
        _isGameRunning = true;
        UIMgr.Instance.HideMenu(GameMenus.PauseMenu);
    }



}