using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;


public class InputMgr : Singleton<InputMgr>
{
    InputAction _shipMoveAction;
    InputAction _mothershipMoveAction;
    InputAction _dropAction;

    public override void Awake() {
        base.Awake();
    }

    private void Start() {
        _shipMoveAction = InputSystem.actions.FindAction( "ShipMove" );
        _mothershipMoveAction = InputSystem.actions.FindAction( "MothershipMove" );
        _dropAction = InputSystem.actions.FindAction("Drop");
    }

    private void Update() {
        PlayerMgr.Instance.ShipMove( _shipMoveAction.ReadValue<Vector2>());
        PlayerMgr.Instance.MothershipMove( _mothershipMoveAction.ReadValue<float>() );
        PlayerMgr.Instance.Drop(_dropAction.ReadValue<float>());
    }
}