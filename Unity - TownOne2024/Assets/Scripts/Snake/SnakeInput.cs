using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeInput : MonoBehaviour
{
    [SerializeField] private SnakePlayer _snakePlayer;

    private void OnControlsChanged()
    {
        // change input controls for UI
    }

    public void OnNavigateX(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float movement = context.ReadValue<float>();
            Debug.Log("OnNavigateX called: " + movement);
            _snakePlayer.PlayerXInput(movement);
        }
    }
    
    public void OnNavigateY(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float movement = context.ReadValue<float>();
            Debug.Log("OnNavigateY called: " + movement);
            _snakePlayer.PlayerYInput(movement);
        }
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _snakePlayer.Drop();
        }
    }
}
