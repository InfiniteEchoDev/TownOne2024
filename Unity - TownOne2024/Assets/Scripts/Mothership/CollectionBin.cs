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
                }
                Destroy(other.gameObject);
            }
            else
            {
                //TODO:Ship resets position somehow
            }
        }

    }
}
