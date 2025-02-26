using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] GameObject meteor;
    [SerializeField] float gravMult = 5f, maxInstTimer = 8f;

    BoxCollider boxCol;

    float timer;

    private void Start()
    {
        boxCol = GetComponent<BoxCollider>();

        gravMult = Random.Range(20, 100);

    }

    private void Update()
    {
        SpawnInterval();
    }

    void SpawnInterval()
    {
        if (timer <= 0)
        {
            SpawnMeteor();
            timer = Random.Range(4, maxInstTimer);
        }
        else
        {
            timer -= Time.deltaTime;

        }
    }

    void SpawnMeteor()
    {
        GameObject inst = Instantiate(meteor, RandomArea(), Random.rotation, gameObject.transform);

        Rigidbody rb = inst.GetComponent<Rigidbody>();

        Vector3 forceDir = new(Random.Range(-20, 20), 1, Random.Range(-20, 20));
        rb.AddForce(forceDir + (Vector3.down * gravMult), ForceMode.VelocityChange);

        gravMult = Random.Range(20, 100);

        Destroy(inst, 10f);
    }

    Vector3 RandomArea()
    {
        float x = Random.Range(boxCol.bounds.min.x, boxCol.bounds.max.x);
        float z = Random.Range(boxCol.bounds.min.z, boxCol.bounds.max.z);

        return new(x, boxCol.center.y, z);
    }

    
}
