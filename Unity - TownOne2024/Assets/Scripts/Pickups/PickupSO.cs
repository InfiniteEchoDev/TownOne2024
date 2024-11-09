using UnityEngine;


[CreateAssetMenu(menuName = "Create PickupSO", fileName ="PickupSO", order = 0)]

public class PickupSO : ScriptableObject
{
    // Pickup type
    public PickupTypes pickupType;

    // Pickup sprite
    public Sprite sprite;


    // Has timer?
    public bool hasTimer;

    // Timer 
    public float timer;

    // Points
    public float pointValue;

    // Spawn Weight
    public int spawnWeight;
}

public enum PickupTypes
{
    Scrap,
    Cargo,
    Human
}