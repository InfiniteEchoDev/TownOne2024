using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

    [SerializeField] GameObject explosionParticles;

    private Vector2 _playerPosition = Vector2.zero;
    private Vector2 _previousPosition = Vector2.zero;

    private Vector2Int _previousCoordinates;
    private Vector2Int _coordinates;
    
    // Private Components
    private Rigidbody2D _rigidbody;

    private CardinalDirection _previousDirection;
    private CardinalDirection _currentDirection = CardinalDirection.North;
    private CardinalDirection _lastInput;

    private LinkedList<Pickup> _snakeBody = new ();
    private HashSet<Vector2Int> _bodyPositions = new ();

    private Animator _animator = default;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

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
            if( _lastInput != CardinalDirection.West ) {
                _currentDirection = _lastInput;
                _lastInput = CardinalDirection.East;
            }
        } else if (xInput < -_deadZone)
        {
            // Left Move
            if( _lastInput != CardinalDirection.East ) {
                _currentDirection = _lastInput;
                _lastInput = CardinalDirection.West;
            }
        }
    }

    public void PlayerYInput(float yInput)
    {
        if (yInput > _deadZone)
        {
            // Right move
            if( _lastInput != CardinalDirection.South ) {
                _currentDirection = _lastInput;
                _lastInput = CardinalDirection.North;
            }
        } else if (yInput < -_deadZone)
        {
            // Left Move
            if( _lastInput != CardinalDirection.North ) {
                _currentDirection = _lastInput;
                _lastInput = CardinalDirection.South;
            }
        }
    }
    
    private void MovePlayerWithTimer()
    {
        _previousDirection = _lastInput;
        var currentCoords = _coordinates;
        
        var nextPos = _snakeGrid.NextValidPositionInDirection(_lastInput, _coordinates);
        if( nextPos == null ) {
            var validPosList = _snakeGrid.GetAllValidMovementPosFromPos( _coordinates );
            validPosList.Remove( validPosList.First( validPos => validPos.pos == _previousCoordinates ) );

            int randIdx = Random.Range( 0, validPosList.Count );
            _lastInput = validPosList[randIdx].dir;
            nextPos = validPosList[randIdx].pos;
        }

        transform.position = new Vector3(nextPos.Value.x * _snakeGrid.CellWidth, nextPos.Value.y * _snakeGrid.CellHeight, 0);
        _previousCoordinates = _coordinates;
        _coordinates = nextPos.Value;


        // Animate turn
        if( _currentDirection != _lastInput ) {
            switch( _lastInput ) {
                case CardinalDirection.North:
                    _animator.SetTrigger( "TurnToNorth" );
                    CoroutineUtilities.WaitAFrameAndExecute( () => _animator.ResetTrigger( "TurnToNorth" ) );
                    break;
                case CardinalDirection.East:
                    _animator.SetTrigger( "TurnToEast" );
                    CoroutineUtilities.WaitAFrameAndExecute( () => _animator.ResetTrigger( "TurnToEast" ) );
                    break;
                case CardinalDirection.South:
                    _animator.SetTrigger( "TurnToSouth" );
                    CoroutineUtilities.WaitAFrameAndExecute( () => _animator.ResetTrigger( "TurnToSouth" ) );
                    break;
                case CardinalDirection.West:
                    _animator.SetTrigger( "TurnToWest" );
                    CoroutineUtilities.WaitAFrameAndExecute( () => _animator.ResetTrigger( "TurnToWest" ) );
                    break;
            }
        }

        _currentDirection = _lastInput;


        if (_bodyPositions.Contains(_coordinates))
        {
            // Crashed into our own tail!
            while (_snakeBody.Count > 0)
            {
                Drop();
            }
            GameLoopManager.Instance.RemoveLives();
            // todo play explosion!
            AudioMgr.Instance.PlaySound(AudioMgr.SoundTypes.PlayerLoseLife);
            CameraShake.Shake(0.25f,1f);
            GameObject explosion = Instantiate(explosionParticles, transform.position, Quaternion.identity);
            Destroy(explosion, 3f);
        }
        
        // check if object in coord
        if (_pickupSpawner.SpawnedPickedUpsDict.Remove(_coordinates, out var pickup))
        {
            // pickup!
            _snakeGrid.OccupiedPositions.Remove(_coordinates);
            
            pickup.transform.position = new Vector3 (currentCoords.x * _snakeGrid.CellWidth, currentCoords.y * _snakeGrid.CellHeight, 0);
            pickup.OnPickup();
            _snakeBody.AddLast(pickup);
        }
        
        // player occupies it
        _snakeGrid.OccupiedPositions.Add(_coordinates);
        
        // todo, asteroids?

        // body position updates
        _bodyPositions.Clear();
        Vector2Int partCoords = currentCoords;
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
