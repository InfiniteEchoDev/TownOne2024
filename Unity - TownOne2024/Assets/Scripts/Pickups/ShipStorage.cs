using UnityEngine;
using System.Collections.Generic;
public class ShipStorage : MonoBehaviour
{

    [SerializeField]
    List<Pickup> pickups = new List<Pickup>();

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.GetComponent<Pickup>())
        {
            Debug.Log("Suffering");
            pickups.Add(other.GetComponent<Pickup>());
            other.gameObject.SetActive(false);
        }

        


    }


    void DropPickUp()
    {
        if (pickups.Count > 0)
        {
            
            pickups[0].transform.position = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
            pickups[0].gameObject.SetActive(true);
            pickups[0].gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            pickups.RemoveAt(0);
        }
    }
}
