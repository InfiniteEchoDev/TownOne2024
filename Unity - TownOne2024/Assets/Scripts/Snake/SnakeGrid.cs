using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SnakeGrid : MonoBehaviour
{
    [SerializeField] private int _gridWidth = 20;
    [SerializeField] private int _gridHeight = 20;
    [SerializeField] private int _gridSize = 1;
    [SerializeField] private float _gridUpdateTime = 1f;

    public Vector2Int GridTotal => new Vector2Int(_gridWidth,_gridHeight);

    private HashSet<Vector2Int> _gridPositions = new();
    public HashSet<Vector2Int> GridPositions => _gridPositions;
    
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
        _timer += Time.deltaTime;
        if (_timer >= _gridUpdateTime)
        {
            _timer = 0f;
            TimerReset?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        for (int i = 0; i < _gridWidth; i++)
        {
            for (int j = 0; j < _gridHeight; j++)
            {
                Gizmos.DrawWireCube(new Vector3(i * _gridWidth, j * _gridHeight, 0) , new Vector3(_gridWidth, _gridHeight, 1));
            }
        }
    }

    public Vector2Int NextPositionInDirection(CardinalDirection direction, Vector2Int position)
    {
        switch (direction)
        {
            case CardinalDirection.North:
                position += position + Vector2Int.up;
                break;
            case CardinalDirection.East:
                position += position + Vector2Int.right;
                break;
            case CardinalDirection.South:
                position += position + Vector2Int.down;
                break;
            case CardinalDirection.West:
                position += position + Vector2Int.left;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        return position;
    }
}
