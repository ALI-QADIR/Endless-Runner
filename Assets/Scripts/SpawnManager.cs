using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // public variables
    [Tooltip("The prefab to spawn")] public GameObject obstaclePrefab;
    [Tooltip("Spawn position")] public Vector3 spawnPos = new(25, 0, 0);
    [Tooltip("Delay before starting spawn")] public float startDelay = 2f;
    [Tooltip("Spawn delay")] public float spawnDelay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), startDelay, spawnDelay);
    }


    void SpawnObstacle()
    {
        Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
