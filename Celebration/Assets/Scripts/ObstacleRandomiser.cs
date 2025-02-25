using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRandomiser : MonoBehaviour
{
    [SerializeField] Transform[] possibleSpawnLocations;
    [SerializeField] GameObject[] obstacles;

    int spawnCount;

    private void Start()
    {
        spawnCount = Random.Range(0, spawnCount);

        SpawnObstacles();
    }

    void SpawnObstacles()
    {
        for (int i = 0; i < possibleSpawnLocations.Length; i++)
        {
            Instantiate(obstacles[Random.Range(0,obstacles.Length)], possibleSpawnLocations[i].position, Quaternion.identity, possibleSpawnLocations[i]);

        }


    }


}
