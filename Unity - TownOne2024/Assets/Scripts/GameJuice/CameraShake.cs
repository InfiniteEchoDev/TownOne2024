using UnityEngine;
using DG.Tweening;

public class CameraShake : Singleton<CameraShake>
{
    private void OnShake(float duration, float strength)
    {
        if (transform != null)
           transform.DOShakePosition(duration, strength);
    }

    public static void Shake(float duration, float strength) => Instance.OnShake(duration, strength);
}
