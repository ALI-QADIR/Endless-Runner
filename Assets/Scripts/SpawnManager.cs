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

    // private variables
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        // get player controller script
        playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        InvokeRepeating(nameof(SpawnObstacle), startDelay, spawnDelay);
    }


    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false) 
        { 
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
