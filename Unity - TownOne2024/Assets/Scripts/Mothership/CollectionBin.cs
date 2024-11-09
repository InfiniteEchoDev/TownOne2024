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
            Destroy(other.gameObject);
        }
        //{
        //    Debug.Log("ship crash)
        //TODO: Ship lose life
        //}
        //else if (other.publicEnumType == type)
        //{
        //    Debug.Log("Survivor is collected)

        //}
        //else{

        //Debug.Log("They're dead now, cause of you, you should feel bad");
        //TODO: Losing score
        //}
    }
}
