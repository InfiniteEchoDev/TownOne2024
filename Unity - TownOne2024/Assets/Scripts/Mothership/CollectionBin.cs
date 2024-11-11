using UnityEngine;
using System.Collections.Generic;
public class CollectionBin : MonoBehaviour
{
    [SerializeField]
    PolygonCollider2D col;

    [SerializeField]
    PickupTypes collectType;
    
    private PickupSpawner _collectSpawner;

    GameMgr gameMgr;

    float scoreMultiplier = 1f;
    
    void Start()
    {
        if (GameMgr.Instance == null)
        {
            Debug.Log("GameMgr not loaded in yet problems");
        }
        else if (GameMgr.Instance != null)
        {
            Debug.Log("We load Gamemgrman");
            gameMgr = GameMgr.Instance;
        }

    }

    private void SetManager(PickupSpawner collectSpawner)
    {
        _collectSpawner = collectSpawner;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Pickup p = other.GetComponent<Pickup>();
        if (p != null && p.GetPickupType == collectType)
        {
            Debug.Log("GOOD HAPPEN");
            //TODO: Gain Score

            if (collectType == PickupTypes.Human)
            {
                AudioMgr.Instance.PlaySound(AudioMgr.SoundTypes.PersonSaved,0.5f);
            }
            else
            {
                AudioMgr.Instance.PlaySound(AudioMgr.SoundTypes.CorrectObject,0.5f);
            }

            gameMgr.AddScore(p.PointValue);
            DestroyPickup(p);
        }
        else
        {
            Debug.Log("BAD HAPPEN");
            
            if (p != null)
            {
                if (p.GetPickupType == PickupTypes.Human)
                {
                    gameMgr.SubtractScore(p.BasePointValue);
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
