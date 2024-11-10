using UnityEngine;
using System.Collections;

public class GameLoopManager : MonoBehaviour
{

    [SerializeField] float gameTimer;
    [SerializeField] int lives = 5;

    private void Start()
    {
        GameMgr.Instance.StartGame();
    }

    private void Update()
    {
        if (GameMgr.Instance.IsGameRunning)
        {
            gameTimer += 1 * Time.deltaTime; // Count up for now, may change later.

            if (lives <= 0)
            {
                GameMgr.Instance.GameOver();
            }
        }
    }

    public void RemoveLives()
    {
        lives -= 1;
    }

}
