using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] pickups;

    [SerializeField] float spawnTimeMin;
    [SerializeField] float spawnTimeMax;

    [SerializeField] float spawnBufferRadius = 1f;

    [SerializeField] bool canSpawn = true;
    Vector3 randomPos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnPickupRoutine());
    }

    // Spawning routine
    IEnumerator SpawnPickupRoutine()
    {
        while (canSpawn)
        {
            // Use random range to chec
            float randomTime = Random.Range(spawnTimeMin, spawnTimeMax);
            yield return new WaitForSeconds(randomTime);
            int randomPickup = GetWeightedRandomPickup();
            SetObjectSpawnPosition();
            GameObject newPickup = Instantiate(pickups[randomPickup], randomPos, Quaternion.identity);
        }

    }

    // Get gameobject based on a weighted probability
    int GetWeightedRandomPickup()
    {
        List<int> weights = new List<int>();
        for(int i = 0; i < pickups.Length; i++)
        {
            weights.Add(pickups[i].GetComponent<Pickup>().GetPickupConfig.spawnWeight);
        }

        int totalWeight = 0;
        
        foreach(int weight in weights)
        {
            totalWeight += weight;
        }
        

        int randomValue = Random.Range(0, totalWeight);
        int runningTotal = 0;

        for(int i = 0; i < weights.Count; i++)
        {
            runningTotal += weights[i];

            if(randomValue < runningTotal)
            {
                return i;
            }
        }

        return 0;

    }

    // Check if an object is at position already and then set a random position
    void SetObjectSpawnPosition()
    {
        //float x = Random.Range(0, Screen.width);
        //float y = Random.Range(0, Screen.height);

        //Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 10));
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 10));

        //Mathf.Round(targetPosition.x);
        //Debug.Log(Mathf.Round(targetPosition.x));
        targetPosition = new Vector3(Mathf.Round(targetPosition.x) + 0.5f, Mathf.Round(targetPosition.y) + 0.5f, 0);
        //Mathf.Round(targetPosition.y);
        //Debug.Log(Mathf.Round(targetPosition.y));

        Collider[] checkIfIntersecting = Physics.OverlapSphere(targetPosition, spawnBufferRadius);

        if(checkIfIntersecting.Length != 0)
        {
            SetObjectSpawnPosition();
        }
        else
        {
            randomPos = targetPosition;
        }
    }
    
}
