using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms.Impl;

public class GameLoopManager : Singleton<GameLoopManager>
{
    [FormerlySerializedAs("gameTimer")] [SerializeField] float GameTimer;
    [FormerlySerializedAs("lives")] [SerializeField] int Lives = 5;

    [FormerlySerializedAs("maxTimer")] [SerializeField] float MaxTimer = 120f;
    [FormerlySerializedAs("isCountdownTimer")] [SerializeField] bool IsCountdownTimer;

    public int GetLives => Lives;

    private void Start()
    {
        // TODO don't call this if loading from another scene
        UIMgr.Instance.ShowMenu(GameMenus.InGameUI);
        
        GameMgr.Instance.StartGame();

        AudioMgr.Instance.PlayMusic(AudioMgr.MusicTypes.RunGameplay, 0.5f);

        if (IsCountdownTimer)
        {
            GameTimer = MaxTimer;
        }

    }

    private void Update()
    {
        if (GameMgr.Instance.IsGameRunning)
        {
            if (!IsCountdownTimer)
            {
                GameTimer += 1 * Time.deltaTime; // Count up for now, may change later.
            }
            else
            {
                GameTimer -= 1 * Time.deltaTime;

                if(GameTimer <= 0)
                {
                    GameMgr.Instance.NextLevel();
                }
            }

            if (Lives <= 0)
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
        Lives -= 1;
    }

    public void ResetLives()
    {
        Lives = 5;
    }

}
