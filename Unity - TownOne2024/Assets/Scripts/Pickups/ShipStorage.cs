using System;
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
            var pickup = other.GetComponent<Pickup>();
            
            
            pickups.Add(pickup);
            other.gameObject.SetActive(false);
        }

        


    }


    public void DropPickUp()
    {
        if (pickups.Count > 0)
        {
            pickups[0].transform.position = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
            pickups[0].gameObject.SetActive(true);
            pickups[0].gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            pickups[0].gameObject.GetComponent<Rigidbody2D>().gravityScale = 5f;
            pickups.RemoveAt(0);
        }
    }
}
