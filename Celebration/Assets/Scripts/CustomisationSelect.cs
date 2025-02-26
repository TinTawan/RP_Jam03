using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CustomisationSelect : MonoBehaviour
{
    [SerializeField] private GameObject[] slides, buttons;

    [SerializeField] private TextMeshProUGUI itemName;


    private int currentSlide;

    EventSystem eventSystem;

    private void Start()
    {
        currentSlide = 0;
        foreach (GameObject g in slides)
        {
            g.SetActive(false);
        }

        slides[currentSlide].SetActive(true);

    }

    /*private void OnEnable()
    {
        Cursor.visible = true;
    }
    private void OnDisable()
    {
        Cursor.visible = false;
    }*/

    private void Update()
    {
        if (currentSlide == 0)
        {
            buttons[0].SetActive(false);
        }
        else
        {
            buttons[0].SetActive(true);
        }

        if (currentSlide == slides.Length - 1)
        {
            buttons[1].SetActive(false);
        }
        else
        {
            buttons[1].SetActive(true);
        }

        itemName.text = slides[currentSlide].name;

        /*if (gameObject.activeInHierarchy)
        {
            if (Gamepad.current.leftShoulder.wasPressedThisFrame)
            {
                LeftButton();
            }
            if (Gamepad.current.rightShoulder.wasPressedThisFrame)
            {
                RightButton();
            }

        }*/


        if (!EventSystem.current.currentSelectedGameObject.activeSelf)
        {
            if (!buttons[0].activeSelf)
            {
                buttons[1].GetComponent<Button>().Select();
            }
            if (!buttons[1].activeSelf)
            {
                buttons[0].GetComponent<Button>().Select();
            }
        }
    }

    public void LeftButton()
    {
        if (currentSlide != 0)
        {
            currentSlide--;

            slides[currentSlide + 1].SetActive(false);
            slides[currentSlide].SetActive(true);
        }

        //FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.buttonPress, transform.position, 1f);

    }
    public void RightButton()
    {
        if (currentSlide != slides.Length - 1)
        {
            currentSlide++;

            slides[currentSlide - 1].SetActive(false);
            slides[currentSlide].SetActive(true);
        }

        //FindObjectOfType<SoundManager>().PlaySound(SoundManager.soundType.buttonPress, transform.position, 1f);

    }



}
