using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;


public class InputMgr : Singleton<InputMgr>
{
    InputAction _shipMoveAction;
    InputAction _mothershipMoveAction;

    public override void Awake() {
        base.Awake();
    }


    private void Start() {
        _shipMoveAction = InputSystem.actions.FindAction( "ShipMove" );
        _mothershipMoveAction = InputSystem.actions.FindAction( "MothershipMove" );
    }

    private void Update() {
        PlayerMgr.Instance.ShipMove( _shipMoveAction.ReadValue<Vector2>() );

        PlayerMgr.Instance.MothershipMove( _mothershipMoveAction.ReadValue<float>() );
    }
}