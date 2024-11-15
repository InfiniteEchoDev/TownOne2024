using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class SnakeInput : MonoBehaviour
{
    [FormerlySerializedAs("_snakePlayer")] [SerializeField] private SnakePlayer SnakePlayer;
    [FormerlySerializedAs("_dropCooldown")] [SerializeField] private float DropCooldown = 0.2f;

    private float _timer = 0f;
    private bool _canDrop = true;
    private void Update()
    {
        if (GameMgr.Instance.IsGameRunning == false) return;
        if (!_canDrop)
        {
            _timer += Time.deltaTime;
            if (_timer >= DropCooldown)
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
            SnakePlayer.PlayerXInput(movement);
        }
    }
    
    public void OnNavigateY(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float movement = context.ReadValue<float>();
            Debug.Log("OnNavigateY called: " + movement);
            SnakePlayer.PlayerYInput(movement);
        }
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        if (_canDrop)
        {
            if (context.performed)
            {
                _canDrop = false;
                SnakePlayer.Drop(true);
            }
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameMgr.Instance.PauseGame();
        }
    }
}
