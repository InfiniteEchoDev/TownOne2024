using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameUI : MenuBase
{
    [FormerlySerializedAs("displayLives")] [SerializeField]
    TMP_Text DisplayLives;
    [FormerlySerializedAs("displayScore")] [SerializeField]
    TMP_Text DisplayScore;

    [FormerlySerializedAs("_addedScoreParent")] [SerializeField] private Transform AddedScoreParent;
    [FormerlySerializedAs("_addedScoreMsg")] [SerializeField] private TMP_Text AddedScoreMsg;

    GameLoopManager _gameLoopManager;
    GameMgr _gameMgr;

    private int _previousScore = 0;
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
            _gameLoopManager = GameLoopManager.Instance;
        }
        if (GameMgr.Instance == null)
        {
            Debug.Log("GameMgr not loaded in yet problems");
        }
        else if (GameMgr.Instance != null)
        {
            Debug.Log("We load Gamemgrman");
            _gameMgr = GameMgr.Instance;
        }
        OnScoreChange(0,0);
    }

    private void Update()
    {
        if (GameLoopManager.Instance != null)
        {

            string livesCount;

            livesCount = "x " + _gameLoopManager.GetLives;

            DisplayLives.text = livesCount;

        }

        if (_gameMgr != null)
        {
            string score;
            int integerScore = Mathf.FloorToInt(_gameMgr.Score);

            if (_previousScore != integerScore)
            {
                OnScoreChange(integerScore, integerScore-_previousScore);
                _previousScore = integerScore;
            }
        }
    }

    private void OnScoreChange(int score, int diff)
    {
        if (diff != 0)
        {
            var msg = Instantiate(AddedScoreMsg, AddedScoreParent, false);
            if (diff < 0)
            {
                msg.color = Color.red;
            }

            msg.text = diff > 0 ? $"+{diff}" : $"{diff}";
            msg.gameObject.SetActive(true);
        }

        DisplayScore.text = score.ToString();
    }
}
