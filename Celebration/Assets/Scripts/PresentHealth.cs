using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PresentHealth : MonoBehaviour
{
    PlayerMovement pMovement;
    PresentAnim presentAnim;
    GrabPresent grabPresent;

    [SerializeField] int health = 8;
    [SerializeField] Slider healthSlider;
    [SerializeField] float canvasRotSmoothing = 5f;
    [SerializeField] GameObject loseMenu;

    [SerializeField] GameObject[] peices;
    bool lost;
    private void Start()
    {
        presentAnim = GetComponent<PresentAnim>();
        healthSlider.maxValue = health;

        loseMenu.SetActive(false);
        lost = false;
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
                grabPresent = fj.connectedBody.GetComponent<GrabPresent>();
            }
        }

        if(health <= 0)
        {
            StartCoroutine(Lose());
        }
        else
        {
            healthSlider.value = health;

            if(pMovement != null && healthSlider != null)
            {
                Canvas canvas = healthSlider.GetComponentInParent<Canvas>();

                Vector3 look = (canvas.transform.position - Camera.main.transform.position).normalized;
                Quaternion rot = Quaternion.LookRotation(look);
                Quaternion faceCam = Quaternion.Slerp(canvas.transform.rotation, rot, canvasRotSmoothing * Time.deltaTime);

                Vector3 yRot = new(0, faceCam.eulerAngles.y, 0);

                canvas.transform.rotation = Quaternion.Euler(yRot);
            }
            
        }




    }

    private void OnCollisionEnter(Collision col)
    {
        if(pMovement != null)
        {
            if (col.gameObject.CompareTag("Ground") && pMovement.GetDropped())
            {
                grabPresent.SetHeld(false);
                health--;
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

    IEnumerator Lose()
    {
        Time.timeScale = 0.75f;

        //break present apart
        GetComponent<MeshRenderer>().enabled = false;
        foreach (GameObject piece in peices)
        {
            piece.SetActive(true);
        }

        //yield return new WaitForSeconds(1f);

        //stop player movement 
        pMovement.StopInputs();

        yield return new WaitForSeconds(2f);

        //show lose screen
        loseMenu.SetActive(true);
        lost = true;
    }

    public int GetHealth()
    {
        return health;
    }

    public bool GetLost()
    {
        return lost;
    }
}
