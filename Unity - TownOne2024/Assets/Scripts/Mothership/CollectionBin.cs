using UnityEngine;
using System.Collections.Generic;
public class CollectionBin : MonoBehaviour
{
    [SerializeField]
    PolygonCollider2D col;

    [SerializeField]
    PickupTypes collectType;
    void Start()
    {
        
        col.autoTiling = true;

    }

    public void SetPickUpType(PickupTypes pickup)
    {
        collectType = pickup;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.enabled = false;
        if (other.GetComponent<Pickup>().GetPickupType == collectType)
        {
            Debug.Log("I HAPPEN");
            //TODO: Gain Score
            Destroy(other.gameObject);
        }
        else
        {
            Debug.Log("BAD HAPPEN");
            if (other.GetComponent<Pickup>().GetPickupType != null)
            {
                //TODO: LOSE SCORE
                Destroy(other.gameObject);
            }
            else
            {
                //TODO:Ship resets position somehow
            }
        }

    }
}
