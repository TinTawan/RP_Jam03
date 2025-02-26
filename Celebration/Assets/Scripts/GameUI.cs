using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameUI : MonoBehaviour
{
    PlayerControls pControls;

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
        presImage.fillAmount = (float)presHealth.GetHealth()/8;
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
