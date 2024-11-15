using System;
using UnityEngine;
using System.Collections.Generic;
public class ShipStorage : MonoBehaviour
{
    private readonly List<Pickup> _pickups = new ();
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Pickup>())
        {
            var pickup = other.GetComponent<Pickup>();
            _pickups.Add(pickup);
        }
    }

    public void DropPickUp()
    {
        if (_pickups.Count > 0)
        {
            _pickups[0].transform.position = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
            _pickups[0].gameObject.SetActive(true);
            _pickups[0].gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            _pickups[0].gameObject.GetComponent<Rigidbody2D>().gravityScale = 5f;
            _pickups.RemoveAt(0);
        }
    }
}
