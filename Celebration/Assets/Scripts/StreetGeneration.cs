using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetGeneration : MonoBehaviour
{
    [SerializeField] GameObject[] streetChunks; //0 = forward road, 1 = cross road
    [SerializeField] Transform startPos;
    [Tooltip("Number of chunks to spawn")] [SerializeField] int streetLength;
    [Tooltip("Percentage chance for a Cross Roads chunk to spawn")] [SerializeField] [Range(1, 100)] int chanceForCR = 30;


    private void Start()
    {
        GenerateRandomStreet();

    }

    void GenerateRandomStreet()
    {
        for(int i = 0; i < streetLength; i++)
        {
            Vector3 pos = new(startPos.position.x, startPos.position.y, startPos.position.z + (i * 30));
            /*GameObject chunk = */Instantiate(RandChunk(), pos, Quaternion.identity, transform);


        }
        
    }

    GameObject RandChunk()
    {
        //more chance for forward road than cross road
        if(Random.Range(0,100) > chanceForCR)
        {
            return streetChunks[0];
        }
        else
        {
            return streetChunks[1];
        }

    }
}
