using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Serialization;


public class PlayerMgr : Singleton<PlayerMgr>
{
    [Header( "Obj Refs" )]
    public PlayerShip PlayerShip;
    public ShipStorage ShipStorage;
    
    [FormerlySerializedAs("_pickupSpawner")] [SerializeField] private PickupSpawner PickupSpawner;

    bool _canPause;

    public override void Awake() {
        base.Awake();
    }

    public void ShipMove( Vector2 inputVec ) {
        PlayerShip.Move( inputVec );
    }
    public void MothershipMove( float inputVec ) {
        // TODO: Connect to PlayerMothership
        // PlayerMothership.Move( inputVec );
    }

    public void Drop(float inputBool)
    {
        if(inputBool == 1f) ShipStorage.DropPickUp();
    }

    public void PauseInput(float inputFloat)
    {
        //Debug.Log(inputFloat);
        // Run pause from game manager
        if (_canPause)
        {
            if (inputFloat == 1) GameMgr.Instance.PauseGame();
            _canPause = false;
        }

        if (inputFloat == 0) _canPause = true;
    }

    public void DestroyPickup(Pickup p)
    {
        PickupSpawner.SpawnedPickedUps.Remove(p);
        Destroy(p.gameObject);
    }
}