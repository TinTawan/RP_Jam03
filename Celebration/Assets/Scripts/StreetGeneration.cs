using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetGeneration : MonoBehaviour
{
    [SerializeField] GameObject[] streetChunks; //0 = forward road, 1 = cross road

    [SerializeField] int streetLength;

    private void Start()
    {
        GenerateRandomStreet();
    }

    void GenerateRandomStreet()
    {
        GameObject chunk = Instantiate(streetChunks[Random.Range(0, streetChunks.Length)])
    }
}
