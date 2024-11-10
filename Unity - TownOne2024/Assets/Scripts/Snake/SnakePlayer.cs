using UnityEngine;

public class SnakePlayer : MonoBehaviour
{
    [Header( "Obj Refs" )]
    public SpriteRenderer _shipSprite;
    public GameObject _attachPoint;
    
    [SerializeField] private int _cameraSize = 20;
    [SerializeField] private float _deadZone = 0.125f;
    [SerializeField] private SnakeGrid _snakeGrid;
    
    private Vector2 _playerPosition = Vector2.zero;
    private Vector2 _previousPosition = Vector2.zero;

    private Vector2Int _coordinates;
    
    // Private Components
    private Rigidbody2D _rigidbody;

    private CardinalDirection _previousDirection;
    private CardinalDirection _lastInput;

    private void Start()
    {
        _snakeGrid.TimerReset += MovePlayerWithTimer;
        _coordinates = _snakeGrid.GridTotal / 2;
        transform.position = new Vector3(Mathf.RoundToInt(_snakeGrid.GridTotal.x / 2f), Mathf.RoundToInt(_snakeGrid.GridTotal.y / 2f), 0);
    }
    
    public void PlayerXInput(float xInput)
    {
        if (xInput > _deadZone)
        {
            // Right move
            _lastInput = CardinalDirection.East;
        }
        else if (xInput < -_deadZone)
        {
            // Left Move
            _lastInput = CardinalDirection.West;
        }
    }

    public void PlayerYInput(float yInput)
    {
        if (yInput > _deadZone)
        {
            // Right move
            _lastInput = CardinalDirection.North;
        }
        else if (yInput < -_deadZone)
        {
            // Left Move
            _lastInput = CardinalDirection.South;
        }
    }
    
    private void MovePlayerWithTimer()
    {
        _previousDirection = _lastInput;
        _snakeGrid.NextPositionInDirection(_lastInput, _coordinates);
    }
    
}
