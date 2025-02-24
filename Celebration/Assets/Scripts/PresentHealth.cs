using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentHealth : MonoBehaviour
{
    PlayerMovement pMovement;
    PresentAnim presentAnim;

    [SerializeField] int health = 8;


    private void Start()
    {
        presentAnim = GetComponent<PresentAnim>();
    }

    private void Update()
    {
        AnimFromHealth();

        if (pMovement == null)
        {
            if(TryGetComponent(out FixedJoint fj))
            {
                GameObject go = fj.connectedBody.gameObject;

                pMovement = go.GetComponentInParent<PlayerMovement>();
            }
        }


    }


    void AnimFromHealth()
    {
        if(health == 7 || health == 8)
        {
            presentAnim.SetState(1);
        }
        if(health == 5 || health == 6)
        {
            presentAnim.SetState(2);
        }
        if (health == 3 || health == 4)
        {
            presentAnim.SetState(3);
        }
        if (health == 1 || health == 2)
        {
            presentAnim.SetState(4);
        }
    }
}
