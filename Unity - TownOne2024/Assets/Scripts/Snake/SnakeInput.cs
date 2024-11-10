using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeInput : Singleton<SnakeInput>
{
    public override void Awake() {
        base.Awake();
    }
    
    private void Update()
    {
        //PlayerMgr.Instance.ShipMove( _shipMoveAction.ReadValue<Vector2>() );
        //PlayerMgr.Instance.MothershipMove( _mothershipMoveAction.ReadValue<float>() );
        //PlayerMgr.Instance.Drop(_dropAction.ReadValue<float>());
    }

    private void OnControlsChanged()
    {
        
    }

    public void OnNavigateX(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float movement = context.ReadValue<float>();
            Debug.Log("OnNavigateX called: " + movement);
            //PlayerMgr.Instance.ShipMove(context.ReadValue<Vector2>());
        }
    }
    
    public void OnNavigateY(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float movement = context.ReadValue<float>();
            Debug.Log("OnNavigateY called: " + movement);
            //PlayerMgr.Instance.ShipMove();
        }
    }
}
