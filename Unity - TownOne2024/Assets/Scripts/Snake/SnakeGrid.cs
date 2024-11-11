using System;
using System.Collections.Generic;
using UnityEngine;

public class SnakeGrid : MonoBehaviour
{
    [SerializeField] private int _gridWidth = 20;
    [SerializeField] private int _gridHeight = 20;
    [SerializeField] private int _cellWidth = 1;
    [SerializeField] private int _cellHeight = 1;
    [SerializeField] private float _gridUpdateTime = 1f;
    [SerializeField] private PickupSpawner _pickupSpawner;

    public int GridWidth => _gridWidth;
    public int GridHeight => _gridHeight;
    public int CellWidth => _cellWidth;
    public int CellHeight => _cellHeight;
    public float GridUpdateTime => _gridUpdateTime;
    public Vector2Int GridTotal => new Vector2Int(_gridWidth,_gridHeight);

    private HashSet<Vector2Int> _gridPositions = new();
    public HashSet<Vector2Int> GridPositions => _gridPositions;
    
    private HashSet<Vector2Int> _occupiedPositions = new();
    public HashSet<Vector2Int> OccupiedPositions => _occupiedPositions;
    
    public Action TimerReset;

    private float _timer = 0f;

    private void Awake()
    {
        for (int i = 0; i < _gridWidth; i++)
        {
            for (int j = 0; j < _gridHeight; j++)
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
            if (_timer >= _gridUpdateTime)
            {
                _occupiedPositions.Clear();
                _timer = 0f;
                TimerReset?.Invoke();
                _pickupSpawner.OnGridTimerReset();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        for (int i = 0; i < _gridWidth; i++)
        {
            for (int j = 0; j < _gridHeight; j++)
            {
                Gizmos.DrawWireCube(new Vector3(i * _cellWidth, j * _cellHeight, 0) , new Vector3(_cellWidth, _cellHeight, 1));
            }
        }
    }

    public Vector2Int NextPositionInDirection(CardinalDirection direction, Vector2Int originalPos)
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
        else
        {
            return originalPos;
        }
    }
}
