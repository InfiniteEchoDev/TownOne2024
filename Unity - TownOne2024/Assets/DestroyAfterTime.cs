using System;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] Vector3 _movementOverTime = Vector3.up;
    [SerializeField] float _destroyAfterTime = 2f;

    private float _timer = 0f;
    
    private CanvasGroup _canvasGroup;

    private void OnEnable()
    {
        _canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    private void Update()
    {
        var pos = transform.position + (_movementOverTime * Time.deltaTime);
        transform.position = pos;
        _timer += Time.deltaTime;
        _canvasGroup.alpha = Mathf.Lerp(1, 0, _timer / _destroyAfterTime);
        if (_timer >= _destroyAfterTime)
        {
            Destroy(gameObject);
        }
    }
}
