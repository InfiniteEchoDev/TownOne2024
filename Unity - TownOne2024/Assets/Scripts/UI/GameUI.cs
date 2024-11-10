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
    GameMgr gameMgr;
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
            Debug.Log("We load gameloopman");
            gameLoopManager = GameLoopManager.Instance;
        }
        if (GameMgr.Instance == null)
        {
            Debug.Log("GameMgr not loaded in yet problems");
        }
        else if (GameMgr.Instance != null)
        {
            Debug.Log("We load gameloopman");
            gameMgr = GameMgr.Instance;
        }
    }

    private void Update()
    {
        if (GameLoopManager.Instance != null)
        {

            string livesCount;

            livesCount = "x " + gameLoopManager.Lives;

            displayLives.text = livesCount;

        }

        if (gameMgr != null)
        {
            string score;
            int integerScore = Mathf.FloorToInt(gameMgr.Score); 
            score = integerScore.ToString();

            displayScore.text = score;
        }

    }


}
