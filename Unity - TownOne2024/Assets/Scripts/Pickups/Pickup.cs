using UnityEngine;

public class Pickup : MonoBehaviour
{

    [SerializeField] PickupSO pickupConfig;

    PickupTypes pickupType;

    Sprite sprite;

    bool hasTimer;

    float timer;

    float pointValue;

    SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateConfig();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateConfig()
    {
        pickupType = pickupConfig.pickupType;
        sprite = pickupConfig.sprite;
        hasTimer = pickupConfig.hasTimer;
        hasTimer = pickupConfig.hasTimer;
        timer = pickupConfig.timer;
    }
}
