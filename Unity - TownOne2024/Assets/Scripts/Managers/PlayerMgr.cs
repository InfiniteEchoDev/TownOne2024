using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class PlayerMgr : Singleton<PlayerMgr>
{


    [Header( "Obj Refs" )]
    public PlayerShip PlayerShip;
    // public PlayerMothership PlayerMothership;

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
}