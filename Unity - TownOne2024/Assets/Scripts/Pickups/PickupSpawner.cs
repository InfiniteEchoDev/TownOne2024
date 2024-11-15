using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PickupSpawner : MonoBehaviour
{
    [FormerlySerializedAs("_grid")] [SerializeField] private SnakeGrid Grid;
    [FormerlySerializedAs("_pickups")] [SerializeField] Pickup[] Pickups;

    [FormerlySerializedAs("_spawnTimeMin")] [SerializeField] int SpawnTimeMin = 3;
    [FormerlySerializedAs("_spawnTimeMax")] [SerializeField] int SpawnTimeMax = 5;

    [FormerlySerializedAs("spawnBufferRadius")] [SerializeField] float SpawnBufferRadius = 1f;

    bool _canSpawn = true;

    Vector3 _randomPos;

    private int _spawnTimer = 0;
    private int NextSpawn => Random.Range(SpawnTimeMin, SpawnTimeMax);
    
    private List<Pickup> _spawnedPickedUps = new ();
    public List<Pickup> SpawnedPickedUps => _spawnedPickedUps;
    
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
            Grid.OccupiedPositions.Add(pickup.SpawnedCoordinates);
        }
        if (_spawnTimer > NextSpawn / Grid.GetGridUpdateTime)
        {
            _spawnTimer = 0;
            SpawnPickup();
        }
    }

    private void SpawnPickup()
    {
        int randomPickup = GetWeightedRandomPickup();
        Vector2Int spawnCoords = SetObjectSpawnPosition();
        Pickup newPickup = Instantiate(Pickups[randomPickup], _randomPos, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
        newPickup.Setup(spawnCoords, Random.Range(10, 30), this);
        Grid.OccupiedPositions.Add(spawnCoords);
        _spawnedPickedUpsDict.Add(spawnCoords, newPickup);
        if(newPickup.GetPickupType == PickupTypes.Human)
        {
            AudioMgr.Instance.PlaySound(AudioMgr.SoundTypes.SpawnPerson);
        }
        else
        {
            AudioMgr.Instance.PlaySound(AudioMgr.SoundTypes.ItemSpawn);
        }
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
        for(int i = 0; i < Pickups.Length; i++)
        {
            weights.Add(Pickups[i].GetComponent<Pickup>().GetPickupConfig.SpawnWeight);
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
            spawnCoords = new Vector2Int(Random.Range(0, Grid.GetGridWidth), Random.Range(0, Grid.GetGridHeight));
        } while (Grid.OccupiedPositions.Contains(spawnCoords));

        Vector3 targetPos = new Vector3(spawnCoords.x * Grid.GetCellWidth, spawnCoords.y * Grid.GetCellHeight, 0);
        
        _randomPos = targetPos;
        return spawnCoords;
    }

    private void Update()
    {
        if (GameMgr.Instance.IsGameRunning)
        {
            _canSpawn = true;
        }
        else
        {
            _canSpawn = false;
        }

    }

    // not for snake body, only uncollected pickups
    public void OnPickupDestroyed(Vector2Int coords, Pickup pickup)
    {
        _spawnedPickedUps.Remove(pickup);
        _spawnedPickedUpsDict.Remove(coords);
        Grid.OccupiedPositions.Remove(coords);
    }
}
