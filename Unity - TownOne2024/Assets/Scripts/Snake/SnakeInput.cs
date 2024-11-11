using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeInput : MonoBehaviour
{
    [SerializeField] private SnakePlayer _snakePlayer;
    [SerializeField] private float _dropCooldown = 0.2f;

    private float _timer = 0f;
    private bool _canDrop = true;
    private void Update()
    {
        if (!_canDrop)
        {
            _timer += Time.deltaTime;
            if (_timer >= _dropCooldown)
            {
                _canDrop = true;
                _timer = 0f;
            }
        }
    }

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
        if (_canDrop)
        {
            if (context.performed)
            {
                _canDrop = false;
                _snakePlayer.Drop();
            }
        }
    }
}
