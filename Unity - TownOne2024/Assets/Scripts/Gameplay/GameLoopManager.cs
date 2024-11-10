using UnityEngine;
using System.Collections;

public class GameLoopManager : MonoBehaviour
{

    [SerializeField] float gameTimer;
    [SerializeField] int lives = 5;

    [SerializeField] float maxTimer = 120f;
    [SerializeField] bool isCountdownTimer;

    private void Start()
    {
        // TODO don't call this if loading from another scene
        UIMgr.Instance.ShowMenu(GameMenus.InGameUI);
        
        GameMgr.Instance.StartGame();

        if (isCountdownTimer)
        {
            gameTimer = maxTimer;
        }

    }

    private void Update()
    {
        if (GameMgr.Instance.IsGameRunning)
        {
            if (!isCountdownTimer)
            {
                gameTimer += 1 * Time.deltaTime; // Count up for now, may change later.
            }
            else
            {
                gameTimer -= 1 * Time.deltaTime;

                if(gameTimer <= 0)
                {
                    GameMgr.Instance.NextLevel();
                }
            }

            if (lives <= 0)
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        GameMgr.Instance.GameOver();
    }

    public void RemoveLives()
    {
        lives -= 1;
    }

}
