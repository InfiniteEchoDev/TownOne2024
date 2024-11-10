using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
            rb.gravityScale = 5f;
        }
        else
        {
            rb.gravityScale = 0f;
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
            yield return new WaitUntil(() => GameMgr.Instance.IsGameRunning);
            spriteRenderer.DOFade(0.5f, 0.33f);
            yield return new WaitForSeconds(0.33f);
            spriteRenderer.DOFade(1f, 0.33f);
            yield return new WaitForSeconds(0.33f);
        }
        yield return new WaitUntil(() => GameMgr.Instance.IsGameRunning);
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
            StartCoroutine(DespawnTimer());
        }
        
        yield return null;
    }
}
