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

    private void Awake()
    {
        UpdateConfig();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

    IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(timer);


    }

    IEnumerator SpawnAnim()
    {

    }
}
