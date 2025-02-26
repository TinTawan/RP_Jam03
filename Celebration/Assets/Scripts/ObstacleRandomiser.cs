using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRandomiser : MonoBehaviour
{
    [SerializeField] Transform[] possibleSpawnLocations;
    [SerializeField] GameObject[] obstacles;

    [SerializeField] float positionDelta = 1f, rotationDelta = 45f;

    int spawnCount;

    private void Start()
    {
        spawnCount = Random.Range(0, possibleSpawnLocations.Length);

        SpawnObstacles();
    }

    void SpawnObstacles()
    {
        for (int i = 1; i < spawnCount; i++)
        {
            Instantiate(obstacles[Random.Range(0,obstacles.Length)], RandPos(possibleSpawnLocations[i].position, positionDelta), RandYRot(Quaternion.identity, rotationDelta), possibleSpawnLocations[i]);

        }

    }

    Vector3 RandPos(Vector3 midPoint, float delta)
    {
        float x = Random.Range(midPoint.x - delta, midPoint.x + delta);
        float z = Random.Range(midPoint.z - delta, midPoint.z + delta);

        return new(x, midPoint.y, z);
    }

    Quaternion RandYRot(Quaternion curRot, float delta)
    {
        Vector3 euler = curRot.eulerAngles;

        float yRot = Random.Range(euler.y - delta, euler.y + delta);

        return Quaternion.Euler(curRot.x, yRot, curRot.z);
    }


}
