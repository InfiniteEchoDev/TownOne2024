using TMPro;
using UnityEngine;

public class GameUI : MenuBase
{
    [SerializeField]
    TMP_Text displayLives;
    [SerializeField]
    TMP_Text displayScore;

    [SerializeField] private Transform _addedScoreParent;
    [SerializeField] private TMP_Text _addedScoreMsg;

    GameLoopManager gameLoopManager;
    GameMgr gameMgr;

    private int previousScore = 0;
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
            Debug.Log("We load Gamemgrman");
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

            if (previousScore != integerScore)
            {
                OnScoreChange(integerScore, previousScore-integerScore);
                previousScore = integerScore;
            }
        }
    }

    private void OnScoreChange(int score, int diff)
    {
        var msg = Instantiate(_addedScoreMsg, _addedScoreParent, false);
        if (diff < 0)
        {
            msg.color = Color.red;
        }
        msg.text = diff > 0 ? $"+{diff}" : $"-{diff}";
        msg.gameObject.SetActive(true);
        
        displayScore.text = score.ToString();
    }
}
