using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private SnakeGrid _grid;
    [SerializeField] Pickup[] _pickups;

    [SerializeField] int _spawnTimeMin = 3;
    [SerializeField] int _spawnTimeMax = 5;

    [SerializeField] float spawnBufferRadius = 1f;

    bool canSpawn = true;

    Vector3 randomPos;

    private int _spawnTimer = 0;
    private int NextSpawn => Random.Range(_spawnTimeMin, _spawnTimeMax);
    
    private List<Pickup> _spawnedPickedUps = new ();
    
    private Dictionary<Vector2Int, Pickup> _spawnedPickedUpsDict = new();
    public Dictionary<Vector2Int, Pickup> SpawnedPickedUpsDict => _spawnedPickedUpsDict;


    private void Start()
    {
        SpawnPickup();
    }

    /// <summary>
    /// Should happen after all other updates for positions to be correct
    /// </summary>
    public void OnGridTimerReset()
    {
        _spawnTimer++;
        foreach (var pickup in _spawnedPickedUps)
        {
            _grid.OccupiedPositions.Add(pickup.SpawnedCoordinates);
        }
        if (_spawnTimer > NextSpawn / _grid.GridUpdateTime)
        {
            _spawnTimer = 0;
            SpawnPickup();
        }
    }

    private void SpawnPickup()
    {
        int randomPickup = GetWeightedRandomPickup();
        Vector2Int spawnCoords = SetObjectSpawnPosition();
        Pickup newPickup = Instantiate(_pickups[randomPickup], randomPos, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
        newPickup.Setup(spawnCoords, Random.Range(10, 30));
        _grid.OccupiedPositions.Add(spawnCoords);
        _spawnedPickedUpsDict.Add(spawnCoords, newPickup);
    }

    // // Spawning routine
    // IEnumerator SpawnPickupRoutine()
    // {
    //     while (canSpawn)
    //     {
    //         // Use random range to check
    //         float randomTime = Random.Range(_s, spawnTimeMax);
    //         yield return new WaitForSeconds(randomTime);
    //         yield return new WaitUntil(() => canSpawn == true);
    //         int randomPickup = GetWeightedRandomPickup();
    //         SetObjectSpawnPosition();
    //         GameObject newPickup = Instantiate(pickups[randomPickup], randomPos, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
    //         newPickup.GetComponent<Pickup>().SetRandomRotSpeed = Random.Range(10, 30);
    //     }
    // }

    // Get gameobject based on a weighted probability
    int GetWeightedRandomPickup()
    {
        List<int> weights = new List<int>();
        for(int i = 0; i < _pickups.Length; i++)
        {
            weights.Add(_pickups[i].GetComponent<Pickup>().GetPickupConfig.spawnWeight);
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
    Vector2Int SetObjectSpawnPosition()
    {
        Vector2Int spawnCoords;
        do
        {
            spawnCoords = new Vector2Int(Random.Range(0, _grid.GridWidth), Random.Range(0, _grid.GridHeight));
        } while (_grid.OccupiedPositions.Contains(spawnCoords));

        Vector3 targetPos = new Vector3(spawnCoords.x * _grid.CellWidth, spawnCoords.y * _grid.CellHeight, 0);
        
        randomPos = targetPos;
        return spawnCoords;
    }

    private void Update()
    {
        if (GameMgr.Instance.IsGameRunning)
        {
            canSpawn = true;
        }
        else
        {
            canSpawn = false;
        }

    }

}
