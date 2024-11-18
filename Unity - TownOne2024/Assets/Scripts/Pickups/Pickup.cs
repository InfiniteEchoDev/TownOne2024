using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.Serialization;

public class Pickup : MonoBehaviour
{
    [FormerlySerializedAs("pickupConfig")] [SerializeField] PickupSo PickupConfig;
    [SerializeField] private float DestroyFloor = -15f;
    public PickupSo GetPickupConfig { get { return PickupConfig; } }

    PickupTypes _pickupType;
    public PickupTypes GetPickupType { get { return _pickupType; } }

    Sprite _sprite;

    bool _hasTimer;
    public bool SetHasTimer { set { _hasTimer = value; } }

    float _timer;

    float _pointValue;

    SpriteRenderer _spriteRenderer;

    float _randomRotSpeed;

    GameLoopManager _loopManager;

    Rigidbody2D _rb;
    
    public Vector2Int SpawnedCoordinates { get; private set; }
    public float PointValue => PickupConfig.PointValue;
    public float BasePointValue => PickupConfig.PointValue;

    private Vector2Int _currentPosition;
    private Vector2Int _previousPosition;
    private SnakeBody _nextSnake;

    private Coroutine _timerRoutine;

    private PickupSpawner _spawnManager;

    public void Setup(Vector2Int coords, float rotSpeed, PickupSpawner spawner)
    {
        _randomRotSpeed = rotSpeed;
        SpawnedCoordinates = coords;
        _spawnManager = spawner;
        UpdateConfig();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _sprite;
        _loopManager = FindAnyObjectByType<GameLoopManager>();
        _rb = GetComponent<Rigidbody2D>();
        _timerRoutine = StartCoroutine(SpawnAnim());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMgr.Instance.IsGameRunning)
        {
            transform.Rotate(new Vector3(0, 0, _randomRotSpeed * Time.deltaTime));
            if (_rb.bodyType == RigidbodyType2D.Dynamic)
            {
                _rb.gravityScale = 5f;
            }
        }
        else
        {
            if (_rb.bodyType == RigidbodyType2D.Dynamic)
            {
                _rb.gravityScale = 0f;
            }
        }

        if (transform.position.y < DestroyFloor)
        {
            _spawnManager.SpawnedPickedUps.Remove(this);
            Destroy(gameObject);
        }
    }

    void UpdateConfig()
    {
        _pickupType = PickupConfig.PickupType;
        _sprite = PickupConfig.Sprite;
        _hasTimer = PickupConfig.HasTimer;
        _hasTimer = PickupConfig.HasTimer;
        _timer = PickupConfig.Timer;
    }

    IEnumerator DespawnTimer()
    {
        yield return new WaitUntil(() => GameMgr.Instance.IsGameRunning);
        yield return new WaitForSeconds(_timer);
        yield return new WaitUntil(() => GameMgr.Instance.IsGameRunning);
        for (int i = 0; i < 9; i++)
        {
            AudioMgr.Instance.PlaySound(AudioMgr.SoundTypes.PersonDying);
            yield return new WaitUntil(() => GameMgr.Instance.IsGameRunning);
            _spriteRenderer.DOFade(0.25f, 0.33f);
            yield return new WaitForSeconds(0.33f);
            _spriteRenderer.DOFade(1f, 0.33f);
            yield return new WaitForSeconds(0.33f);
        }
        yield return new WaitUntil(() => GameMgr.Instance.IsGameRunning);
        AudioMgr.Instance.PlaySound(AudioMgr.SoundTypes.PersonDespawn);
        transform.DOScale(0f, 0.75f);
        _spriteRenderer.DOFade(0f, 0.75f);
        yield return new WaitForSeconds(0.75f);
        _loopManager.RemoveLives();
        _spawnManager.OnPickupDestroyed(_currentPosition, this);
        Destroy(gameObject);
    }

    IEnumerator SpawnAnim()
    {
        yield return new WaitUntil(() => GameMgr.Instance.IsGameRunning);
        transform.DOScale(1f, 0.75f);
        _spriteRenderer.DOFade(1f, 0.75f);
        
        if (_hasTimer)
        {
            _timerRoutine = StartCoroutine(DespawnTimer());
        }
        
        yield return null;
    }

    public void OnPickup()
    {
        if (_timerRoutine != null)
        {
            StopCoroutine(_timerRoutine);
            transform.DOKill();
            transform.DOScale(1f, 0.1f);
            _spriteRenderer.DOKill();
            _spriteRenderer.DOFade(1f, 0.1f);
        }
        if (_hasTimer)
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
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.gravityScale = 5f;
    }
}
