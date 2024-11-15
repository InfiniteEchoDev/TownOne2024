using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(menuName = "Create PickupSo", fileName ="PickupSo", order = 0)]

public class PickupSo : ScriptableObject
{
    // Pickup type
    [FormerlySerializedAs("pickupType")] public PickupTypes PickupType;

    // Pickup sprite
    [FormerlySerializedAs("sprite")] public Sprite Sprite;


    // Has timer?
    [FormerlySerializedAs("hasTimer")] public bool HasTimer;

    // Timer 
    [FormerlySerializedAs("timer")] public float Timer;

    // Points
    [FormerlySerializedAs("pointValue")] public float PointValue;

    // Spawn Weight
    [FormerlySerializedAs("spawnWeight")] public int SpawnWeight;
}

public enum PickupTypes
{
    Scrap,
    Cargo,
    Human
}