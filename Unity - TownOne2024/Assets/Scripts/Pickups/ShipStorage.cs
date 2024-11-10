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
            pickups.Add(other.GetComponent<Pickup>());
            Destroy(other);
        }

    }
}
