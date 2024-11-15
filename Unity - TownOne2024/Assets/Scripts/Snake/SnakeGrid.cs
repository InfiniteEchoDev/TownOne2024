using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SnakeGrid : MonoBehaviour
{
    [FormerlySerializedAs("_gridWidth")] [SerializeField] private int GridWidth = 20;
    [FormerlySerializedAs("_gridHeight")] [SerializeField] private int GridHeight = 20;
    [FormerlySerializedAs("_cellWidth")] [SerializeField] private int CellWidth = 1;
    [FormerlySerializedAs("_cellHeight")] [SerializeField] private int CellHeight = 1;
    [FormerlySerializedAs("_gridUpdateTime")] [SerializeField] private float GridUpdateTime = 1f;
    [FormerlySerializedAs("_pickupSpawner")] [SerializeField] private PickupSpawner PickupSpawner;

    public int GetGridWidth => GridWidth;
    public int GetGridHeight => GridHeight;
    public int GetCellWidth => CellWidth;
    public int GetCellHeight => CellHeight;
    public float GetGridUpdateTime => GridUpdateTime;
    public Vector2Int GridTotal => new Vector2Int(GridWidth,GridHeight);

    private HashSet<Vector2Int> _gridPositions = new();
    public HashSet<Vector2Int> GridPositions => _gridPositions;
    
    private HashSet<Vector2Int> _occupiedPositions = new();
    public HashSet<Vector2Int> OccupiedPositions => _occupiedPositions;
    
    public Action TimerReset;

    private float _timer = 0f;

    private void Awake()
    {
        for (int i = 0; i < GridWidth; i++)
        {
            for (int j = 0; j < GridHeight; j++)
            {
                var pos = new Vector2Int(i, j);
                _gridPositions.Add(pos);
            }
        }
    }

    private void Update()
    {
        if (GameMgr.Instance.IsGameRunning)
        {
            _timer += Time.deltaTime;
            if (_timer >= GridUpdateTime)
            {
                _occupiedPositions.Clear();
                _timer = 0f;
                TimerReset?.Invoke();
                PickupSpawner.OnGridTimerReset();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        for (int i = 0; i < GridWidth; i++)
        {
            for (int j = 0; j < GridHeight; j++)
            {
                Gizmos.DrawWireCube(new Vector3(i * CellWidth, j * CellHeight, 0) , new Vector3(CellWidth, CellHeight, 1));
            }
        }
    }

    public Vector2Int? NextValidPositionInDirection(CardinalDirection direction, Vector2Int originalPos)
    {
        Vector2Int position;
        switch (direction)
        {
            case CardinalDirection.North:
                position = originalPos + Vector2Int.up;
                break;
            case CardinalDirection.East:
                position = originalPos + Vector2Int.right;
                break;
            case CardinalDirection.South:
                position = originalPos + Vector2Int.down;
                break;
            case CardinalDirection.West:
                position = originalPos + Vector2Int.left;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        if (_gridPositions.Contains(position))
        {
            return position;
        }

        return null;
    }

    public List<(Vector2Int pos, CardinalDirection dir)> GetAllValidMovementPosFromPos( Vector2Int fromPos ) {
        List<(Vector2Int pos, CardinalDirection dir)> validPositions = new();

        Vector2Int checkPos = new Vector2Int( fromPos.x + 1, fromPos.y );
        if( _gridPositions.Contains( checkPos ) )
            validPositions.Add( (checkPos, CardinalDirection.East) );

        checkPos = new Vector2Int( fromPos.x - 1, fromPos.y );
        if( _gridPositions.Contains( checkPos ) )
            validPositions.Add( (checkPos, CardinalDirection.West) );

        checkPos = new Vector2Int( fromPos.x, fromPos.y + 1 );
        if( _gridPositions.Contains( checkPos ) )
            validPositions.Add( (checkPos, CardinalDirection.North) );

        checkPos = new Vector2Int( fromPos.x, fromPos.y - 1 );
        if( _gridPositions.Contains( checkPos ) )
            validPositions.Add( (checkPos, CardinalDirection.South) );

        return validPositions;
    }


}
