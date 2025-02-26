using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRandomColour : MonoBehaviour
{
    [SerializeField] int colourIndex = 0;
    void Start()
    {
        Material mat = GetComponent<MeshRenderer>().materials[colourIndex];

        Color col = Random.ColorHSV();
        mat.color = col;
    }

}
