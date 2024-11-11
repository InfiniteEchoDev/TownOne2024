using System.Collections.Generic;
using UnityEngine;

public class SnakePlayer : MonoBehaviour
{
    [Header( "Obj Refs" )]
    public SpriteRenderer _shipSprite;
    public GameObject _attachPoint;
    
    [SerializeField] private int _cameraSize = 20;
    [SerializeField] private float _deadZone = 0.125f;
    [SerializeField] private SnakeGrid _snakeGrid;
    [SerializeField] private PickupSpawner _pickupSpawner;
    
    private Vector2 _playerPosition = Vector2.zero;
    private Vector2 _previousPosition = Vector2.zero;

    private Vector2Int _coordinates;
    
    // Private Components
    private Rigidbody2D _rigidbody;

    private CardinalDirection _previousDirection;
    private CardinalDirection _lastInput;

    private LinkedList<Pickup> _snakeBody = new ();
    private HashSet<Vector2Int> _bodyPositions = new ();

    private void Start()
    {
        _snakeGrid.TimerReset += MovePlayerWithTimer;
        _coordinates = new Vector2Int(_snakeGrid.GridWidth / 2, _snakeGrid.GridHeight / 2);
        transform.position = new Vector3(_coordinates.x * _snakeGrid.CellWidth, _coordinates.y * _snakeGrid.CellHeight, 0);
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
        var previousCoords = _coordinates;
        
        var nextPos = _snakeGrid.NextPositionInDirection(_lastInput, _coordinates);
        transform.position = new Vector3(nextPos.x * _snakeGrid.CellWidth, nextPos.y * _snakeGrid.CellHeight, 0);
        _coordinates = nextPos;

        if (_bodyPositions.Contains(_coordinates))
        {
            // Crashed into our own tail!
            while (_snakeBody.Count > 0)
            {
                Drop();
            }
            GameLoopManager.Instance.RemoveLives();
            // todo play explosion!
        }
        
        // check if object in coord
        if (_pickupSpawner.SpawnedPickedUpsDict.Remove(_coordinates, out var pickup))
        {
            // pickup!
            _snakeGrid.OccupiedPositions.Remove(_coordinates);
            pickup.transform.position = new Vector3 (previousCoords.x * _snakeGrid.CellWidth, previousCoords.y * _snakeGrid.CellHeight, 0);
            pickup.OnPickup();
            _snakeBody.AddLast(pickup);
        }
        
        // player occupies it
        _snakeGrid.OccupiedPositions.Add(_coordinates);
        
        // todo, asteroids?

        // body position updates
        _bodyPositions.Clear();
        Vector2Int partCoords = previousCoords;
        foreach (var body in _snakeBody)
        {
            _bodyPositions.Add(partCoords);
            partCoords = body.SetPosition(partCoords, _snakeGrid.CellWidth, _snakeGrid.CellWidth);
        }
    }

    public void Drop()
    {
        if (_snakeBody.Count > 0)
        {
            var first = _snakeBody.First.Value;
            _snakeBody.Remove(first);
            first.Drop();
        }
    }
}
