using UnityEngine;

public class CollectionBin : MonoBehaviour
{
    [SerializeField]
    PolygonCollider2D col;

    //Public Enum
    
    void Start()
    {
        col.autoTiling = true;
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
