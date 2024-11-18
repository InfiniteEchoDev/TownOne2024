using System;
using UnityEngine;
using UnityEngine.Serialization;

public class DestroyAfterTime : MonoBehaviour
{
    [FormerlySerializedAs("_movementOverTime")] [SerializeField] Vector3 MovementOverTime = Vector3.up;
    [FormerlySerializedAs("_destroyAfterTime")] [SerializeField] float DestroyTime = 2f;

    private float _timer = 0f;
    
    private CanvasGroup _canvasGroup;

    private void OnEnable()
    {
        _canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    private void Update()
    {
        var pos = transform.position + (MovementOverTime * Time.deltaTime);
        transform.position = pos;
        _timer += Time.deltaTime;
        _canvasGroup.alpha = Mathf.Lerp(1, 0, _timer / DestroyTime);
        if (_timer >= DestroyTime)
        {
            Destroy(gameObject);
        }
    }
}
