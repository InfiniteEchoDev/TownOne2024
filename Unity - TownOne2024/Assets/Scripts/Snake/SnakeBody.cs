using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    private Vector2 _currentPosition;
    private Vector2 _previousPosition;

    private SnakeBody _nextSnake;

    public void OnGridMove(Vector2 position)
    {
        _previousPosition = _currentPosition;
        transform.position = _currentPosition = position;
        _nextSnake.OnGridMove(_previousPosition);
    }
}
