using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Serialization;


public class SnakePlayer : MonoBehaviour
{

    [FormerlySerializedAs("_shipSprite")]
    [Header( "Obj Refs" )]
    [SerializeField] SpriteRenderer ShipSprite;
    [FormerlySerializedAs("_attachPoint")] [SerializeField] GameObject AttachPoint;
    
    [FormerlySerializedAs("_cameraSize")] [SerializeField] private int CameraSize = 20;
    [FormerlySerializedAs("_deadZone")] [SerializeField] private float DeadZone = 0.125f;
    [FormerlySerializedAs("_snakeGrid")] [SerializeField] private SnakeGrid SnakeGrid;
    [FormerlySerializedAs("_pickupSpawner")] [SerializeField] private PickupSpawner PickupSpawner;

    [FormerlySerializedAs("explosionParticles")] [SerializeField] GameObject ExplosionParticles;

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
        SnakeGrid.TimerReset += MovePlayerWithTimer;
        _coordinates = new Vector2Int(SnakeGrid.GetGridWidth / 2, SnakeGrid.GetGridHeight / 2);
        transform.position = new Vector3(_coordinates.x * SnakeGrid.GetCellWidth, _coordinates.y * SnakeGrid.GetCellHeight, 0);
    }
    
    public void PlayerXInput(float xInput)
    {
        if (xInput > DeadZone)
        {
            // Right move
            if( _lastInput != CardinalDirection.West ) {
                _currentDirection = _lastInput;
                _lastInput = CardinalDirection.East;
            }
        } else if (xInput < -DeadZone)
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
        if (yInput > DeadZone)
        {
            // Right move
            if( _lastInput != CardinalDirection.South ) {
                _currentDirection = _lastInput;
                _lastInput = CardinalDirection.North;
            }
        } else if (yInput < -DeadZone)
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
        
        var nextPos = SnakeGrid.NextValidPositionInDirection(_lastInput, _coordinates);
        if( nextPos == null ) {
            var validPosList = SnakeGrid.GetAllValidMovementPosFromPos( _coordinates );
            validPosList.Remove( validPosList.First( validPos => validPos.pos == _previousCoordinates ) );

            int randIdx = Random.Range( 0, validPosList.Count );
            _lastInput = validPosList[randIdx].dir;
            nextPos = validPosList[randIdx].pos;
        }

        transform.position = new Vector3(nextPos.Value.x * SnakeGrid.GetCellWidth, nextPos.Value.y * SnakeGrid.GetCellHeight, 0);
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
                Drop(false);
            }
            GameLoopManager.Instance.RemoveLives();
            // todo play explosion!
            AudioMgr.Instance.PlaySound(AudioMgr.SoundTypes.PlayerLoseLife);
            CameraShake.Shake(0.25f,1f);
            GameObject explosion = Instantiate(ExplosionParticles, transform.position, Quaternion.identity);
            Destroy(explosion, 3f);
        }
        
        // check if object in coord
        if (PickupSpawner.SpawnedPickedUpsDict.Remove(_coordinates, out var pickup))
        {
            // pickup!
            SnakeGrid.OccupiedPositions.Remove(_coordinates);
            
            pickup.transform.position = new Vector3 (currentCoords.x * SnakeGrid.GetCellWidth, currentCoords.y * SnakeGrid.GetCellHeight, 0);
            pickup.OnPickup();
            _snakeBody.AddLast(pickup);
        }
        
        // player occupies it
        SnakeGrid.OccupiedPositions.Add(_coordinates);
        
        // todo, asteroids?

        UpdateBody(_previousCoordinates);
    }

    private void UpdateBody(Vector2Int currentCoords)
    {
        // body position updates
        _bodyPositions.Clear();
        Vector2Int partCoords = currentCoords;
        foreach (var body in _snakeBody)
        {
            _bodyPositions.Add(partCoords);
            partCoords = body.SetPosition(partCoords, SnakeGrid.GetCellWidth, SnakeGrid.GetCellWidth);
        }
    }

    public void Drop(bool atPlayer)
    {
        if (_snakeBody.Count > 0)
        {
            var first = _snakeBody.First.Value;
            _snakeBody.Remove(first);
            if (atPlayer)
            {
                first.transform.position = transform.position;
            }
            first.Drop();
        }
        UpdateBody(_previousCoordinates);
    }

}
