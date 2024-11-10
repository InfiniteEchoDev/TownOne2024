using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MenuBase
{

    [SerializeField]
    TMP_Text displayLives;
    [SerializeField]
    TMP_Text displayScore;

    GameLoopManager gameLoopManager;
    public override GameMenus MenuType()
    {
        return GameMenus.InGameUI;
    }

    private void Start()
    {
        if (GameLoopManager.Instance == null) {
            Debug.Log("GameloopManager not loaded in yet problems");
        }
        else if(GameLoopManager.Instance != null)
        {
            gameLoopManager = GameLoopManager.Instance;
        }
    }

    private void Update()
    {
        if (GameLoopManager.Instance != null)
        {
            string livesCount;

            livesCount = "x " + gameLoopManager.Lives;

        }

    }


}
