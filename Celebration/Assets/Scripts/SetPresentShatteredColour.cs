using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPresentShatteredColour : MonoBehaviour
{
    void Start()
    {
        PresentAnim pres = GetComponentInParent<PresentAnim>();
        MeshRenderer mr = GetComponent<MeshRenderer>();

        mr.materials[0].color = pres.GetComponent<MeshRenderer>().materials[0].color;
    }

}
