using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class PlayerMgr : Singleton<PlayerMgr>
{


    [Header( "Obj Refs" )]
    public PlayerShip PlayerShip;
    public ShipStorage ShipStorage;
    // public PlayerMothership PlayerMothership;

    bool canPause;

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
        if (canPause)
        {
            if (inputFloat == 1) GameMgr.Instance.PauseGame();
            canPause = false;
        }

        if (inputFloat == 0) canPause = true;
    }
}