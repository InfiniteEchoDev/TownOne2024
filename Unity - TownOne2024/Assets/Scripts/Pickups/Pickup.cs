using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Pickup : MonoBehaviour
{
    [SerializeField] PickupSO pickupConfig;
    public PickupSO GetPickupConfig { get { return pickupConfig; } }

    PickupTypes pickupType;
    public PickupTypes GetPickupType { get { return pickupType; } }

    Sprite sprite;

    bool hasTimer;
    public bool SetHasTimer { set { hasTimer = value; } }

    float timer;

    float pointValue;

    SpriteRenderer spriteRenderer;

    float randomRotSpeed;

    GameLoopManager loopManager;

    Rigidbody2D rb;
    
    public Vector2Int SpawnedCoordinates { get; private set; }
    public float PointValue => pickupConfig.pointValue;
    public float BasePointValue => pickupConfig.pointValue;

    private Vector2Int _currentPosition;
    private Vector2Int _previousPosition;
    private SnakeBody _nextSnake;

    private Coroutine _timerRoutine;

    public void Setup(Vector2Int coords, float rotSpeed)
    {
        randomRotSpeed = rotSpeed;
        SpawnedCoordinates = coords;
        UpdateConfig();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        loopManager = FindAnyObjectByType<GameLoopManager>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(SpawnAnim());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMgr.Instance.IsGameRunning)
        {
            transform.Rotate(new Vector3(0, 0, randomRotSpeed * Time.deltaTime));
            if (rb.bodyType == RigidbodyType2D.Dynamic)
            {
                rb.gravityScale = 5f;
            }
        }
        else
        {
            if (rb.bodyType == RigidbodyType2D.Dynamic)
            {
                rb.gravityScale = 0f;
            }
        }
    }

    void UpdateConfig()
    {
        pickupType = pickupConfig.pickupType;
        sprite = pickupConfig.sprite;
        hasTimer = pickupConfig.hasTimer;
        hasTimer = pickupConfig.hasTimer;
        timer = pickupConfig.timer;
    }

    IEnumerator DespawnTimer()
    {
        yield return new WaitUntil(() => GameMgr.Instance.IsGameRunning);
        yield return new WaitForSeconds(timer);
        yield return new WaitUntil(() => GameMgr.Instance.IsGameRunning);
        for (int i = 0; i < 9; i++)
        {
            AudioMgr.Instance.PlaySound(AudioMgr.SoundTypes.PersonDying);
            yield return new WaitUntil(() => GameMgr.Instance.IsGameRunning);
            spriteRenderer.DOFade(0.25f, 0.33f);
            yield return new WaitForSeconds(0.33f);
            spriteRenderer.DOFade(1f, 0.33f);
            yield return new WaitForSeconds(0.33f);
        }
        yield return new WaitUntil(() => GameMgr.Instance.IsGameRunning);
        AudioMgr.Instance.PlaySound(AudioMgr.SoundTypes.PersonDespawn);
        transform.DOScale(0f, 0.75f);
        spriteRenderer.DOFade(0f, 0.75f);
        yield return new WaitForSeconds(0.75f);
        loopManager.RemoveLives();
        Destroy(gameObject);

    }

    IEnumerator SpawnAnim()
    {
        yield return new WaitUntil(() => GameMgr.Instance.IsGameRunning);
        transform.DOScale(1f, 0.75f);
        spriteRenderer.DOFade(1f, 0.75f);

        
        if (hasTimer)
        {
            _timerRoutine = StartCoroutine(DespawnTimer());
        }
        
        yield return null;
    }

    public void OnPickup()
    {
        if (_timerRoutine != null)
            StopCoroutine(_timerRoutine);
        if (hasTimer)
        {
            AudioMgr.Instance.PlaySound(AudioMgr.SoundTypes.PersonPickedUp);
        }
        else
        {
            AudioMgr.Instance.PlaySound(AudioMgr.SoundTypes.ItemPickup);
        }
    }
    
    public Vector2Int SetPosition(Vector2Int previousCoords, int cellX, int cellY)
    {
        var lastPosition = _previousPosition;
        _currentPosition = previousCoords;
        _previousPosition = _currentPosition;
        transform.position = new Vector3(_currentPosition.x * cellX, _currentPosition.y * cellY, 0);
        return lastPosition;
    }

    public void Drop()
    {
        AudioMgr.Instance.PlaySound(AudioMgr.SoundTypes.DropItems,0.5f);
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 5f;
    }
}
