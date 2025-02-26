using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameUI : MonoBehaviour
{
    PlayerControls pControls;
    Animator anim;

    [SerializeField] PresentHealth presHealth;
    [SerializeField] Image presImage;

    [SerializeField] GameObject pauseMenu;
    bool isPaused;

    private void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false;

        pControls = new PlayerControls();
        pControls.UI.Enable();
        pControls.UI.Escape.started += Escape_started;

        anim = GetComponentInChildren<Animator>();
    }

    private void Escape_started(InputAction.CallbackContext ctx)
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }


    private void Update()
    {
        //presImage.fillAmount = (float)presHealth.GetHealth()/8;
        AnimatePresentUI();
    }

    void AnimatePresentUI()
    {
        if (presHealth.GetHealth() == 7 || presHealth.GetHealth() == 8)
        {
            anim.SetFloat("state", 0);
        }
        if (presHealth.GetHealth() == 5 || presHealth.GetHealth() == 6)
        {
            anim.SetFloat("state", 1);

        }
        if (presHealth.GetHealth() == 3 || presHealth.GetHealth() == 4)
        {
            anim.SetFloat("state", 2);

        }
        if (presHealth.GetHealth() == 1 || presHealth.GetHealth() == 2)
        {
            anim.SetFloat("state", 3);

        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);

        isPaused = false;

    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);

        isPaused = true;
    }

    public void Menu()
    {
        Debug.Log("Go to menu");
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    private void OnDisable()
    {
        pControls.UI.Disable();

    }
}
