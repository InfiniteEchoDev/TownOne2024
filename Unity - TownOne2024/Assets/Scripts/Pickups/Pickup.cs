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

    float timer;

    float pointValue;

    SpriteRenderer spriteRenderer;

    float randomRotSpeed;
    public float SetRandomRotSpeed { set { randomRotSpeed = value; } }

    private void Awake()
    {
        UpdateConfig();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        StartCoroutine(SpawnAnim());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,randomRotSpeed * Time.deltaTime));
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
        yield return new WaitForSeconds(timer);

        for (int i = 0; i < 9; i++)
        {
            spriteRenderer.DOFade(0.5f, 0.33f);
            yield return new WaitForSeconds(0.33f);
            spriteRenderer.DOFade(1f, 0.33f);
            yield return new WaitForSeconds(0.33f);
        }

        transform.DOScale(0f, 0.75f);
        spriteRenderer.DOFade(0f, 0.75f);
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);

    }

    IEnumerator SpawnAnim()
    {
        transform.DOScale(1f, 0.75f);
        spriteRenderer.DOFade(1f, 0.75f);

        
        if (hasTimer)
        {
            StartCoroutine(DespawnTimer());
        }
        
        yield return null;
    }
}
