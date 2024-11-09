using UnityEngine;
using System.Collections.Generic;
public class CollectionBin : MonoBehaviour
{
    [SerializeField]
    PolygonCollider2D col;

    [SerializeField]
    List<Color> color = new List<Color>();

    PickupTypes collectType;
    void Start()
    {
        
        col.autoTiling = true;

        int ran = Random.Range(0, 3);
        Debug.Log(ran);
        //Depending on type change sprite to whatever
        SpriteRenderer.color = color[ran];
    }

    public void SetPickUpType(PickupTypes pickup)
    {
        collectType = pickup;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("I HAPPEN");
        //if (other.publicEnumType == Null)
        //{
        //    Debug.Log("ship crash)
        //TODO: Ship lose life
        //}
        //else if (other.publicEnumType == type)
        //{
        //    Debug.Log("Survivor is collected)
        //TODO: Gain Score
        //}
        //else{

        //Debug.Log("They're dead now, cause of you, you should feel bad");
        //TODO: Losing score
        //}
    }
}
