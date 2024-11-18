using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class CollectionBin : MonoBehaviour
{
    [FormerlySerializedAs("col")] [SerializeField]
    PolygonCollider2D Col;

    [FormerlySerializedAs("collectType")] [SerializeField]
    PickupTypes CollectType;
    
    private PickupSpawner _collectSpawner;

    GameMgr _gameMgr;

    float _scoreMultiplier = 1f;

    [FormerlySerializedAs("confirmationParticles")] [SerializeField]
    GameObject ConfirmationParticles;

    void Start()
    {
        if (GameMgr.Instance == null)
        {
            Debug.Log("GameMgr not loaded in yet problems");
        }
        else if (GameMgr.Instance != null)
        {
            //Debug.Log("We load Gamemgrman");
            _gameMgr = GameMgr.Instance;
        }

    }

    private void SetManager(PickupSpawner collectSpawner)
    {
        _collectSpawner = collectSpawner;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Pickup p = other.GetComponent<Pickup>();
        if (p != null && p.GetPickupType == CollectType)
        {
            //Debug.Log("GOOD HAPPEN");
            //TODO: Gain Score

            GameObject confirmation = Instantiate(ConfirmationParticles, transform.position, Quaternion.identity);
            Destroy(confirmation, 3f);

            if (CollectType == PickupTypes.Human)
            {
                AudioMgr.Instance.PlaySound(AudioMgr.SoundTypes.PersonSaved,0.5f);
            }
            else
            {
                AudioMgr.Instance.PlaySound(AudioMgr.SoundTypes.CorrectObject,0.5f);
            }

            _gameMgr.AddScore(p.PointValue);
            DestroyPickup(p);
        }
        else
        {
            //Debug.Log("BAD HAPPEN");
            
            if (p != null)
            {
                CameraShake.Shake(0.25f, 0.5f);
                if (p.GetPickupType == PickupTypes.Human)
                {
                    _gameMgr.SubtractScore(p.BasePointValue);
                    AudioMgr.Instance.PlaySound(AudioMgr.SoundTypes.PersonSortingError,0.5f);
                }
                else
                {
                    AudioMgr.Instance.PlaySound(AudioMgr.SoundTypes.SortingError,0.5f);
                }
                DestroyPickup(p);
            }
        }
    }
    
    private void DestroyPickup(Pickup p)
    {
        PlayerMgr.Instance.DestroyPickup(p);
    }
}
