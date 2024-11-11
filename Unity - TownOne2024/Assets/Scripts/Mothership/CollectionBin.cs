using UnityEngine;
using System.Collections.Generic;
public class CollectionBin : MonoBehaviour
{
    [SerializeField]
    PolygonCollider2D col;

    [SerializeField]
    PickupTypes collectType;

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

    public void SetPickUpType(PickupTypes pickup)
    {
        collectType = pickup;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Pickup>().GetPickupType == collectType)
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

            gameMgr.AddScore(other.GetComponent<Pickup>().PointValue);
            Destroy(other.gameObject);
        }
        else
        {
            Debug.Log("BAD HAPPEN");
            if (other.GetComponent<Pickup>() != null)
            {

                if (other.GetComponent<Pickup>().GetPickupType == PickupTypes.Human)
                {
                    gameMgr.SubtractScore(other.GetComponent<Pickup>().BasePointValue);
                    AudioMgr.Instance.PlaySound(AudioMgr.SoundTypes.PersonSortingError,0.5f);
                }
                else
                {
                    AudioMgr.Instance.PlaySound(AudioMgr.SoundTypes.SortingError,0.5f);
                }
                Destroy(other.gameObject);
            }

        }

    }
}
