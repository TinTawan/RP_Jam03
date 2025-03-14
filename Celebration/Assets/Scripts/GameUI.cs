using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    PlayerControls pControls;
    Animator anim;
    EndGoal end;

    [SerializeField] Button pauseButton, loseMenuRestartButton, winMenuPlayAgainButton;
    [SerializeField] PresentHealth presHealth;
    [SerializeField] Image presImage;

    [SerializeField] GameObject pauseMenu, winMenu;
    bool isPaused, doOnce;

    [SerializeField] GameObject pauseSectionPause, pauseSectionSettings;


    private void Start()
    {
        pauseMenu.SetActive(false);
        presImage.gameObject.SetActive(true);

        isPaused = false;

        pControls = new PlayerControls();
        pControls.UI.Enable();
        pControls.UI.Escape.started += Escape_started;

        anim = GetComponentInChildren<Animator>();

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;

        doOnce = true;

        end = FindObjectOfType<EndGoal>();

    }

    private void Escape_started(InputAction.CallbackContext ctx)
    {
        if (!presHealth.GetLost() || !end.GetWon())
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
        
    }


    private void Update()
    {
        AnimatePresentUI();

        if (presHealth.GetLost() && doOnce)
        {
            doOnce = false;
            presImage.gameObject.SetActive(false);

            Lose();
        }

        if(!presHealth.GetLost() && end.GetWon() && doOnce)
        {
            doOnce = false;
            presImage.gameObject.SetActive(false);

            Win();
        }
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
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1f;
        pauseMenu.SetActive(false);

        isPaused = false;

        presImage.gameObject.SetActive(true);

    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.Confined;

        presImage.gameObject.SetActive(false);

        EventSystem.current.SetSelectedGameObject(pauseButton.gameObject);

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

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Lose()
    {
        Cursor.lockState = CursorLockMode.Confined;

        pauseMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(loseMenuRestartButton.gameObject);
        Time.timeScale = 0f;

        pControls.UI.Escape.Disable();

    }
    void Win()
    {
        Cursor.lockState = CursorLockMode.Confined;

        winMenu.SetActive(true);
        pauseMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(winMenuPlayAgainButton.gameObject);
        Time.timeScale = 0f;

        pControls.UI.Escape.Disable();

    }

    public void PauseSectionPause()
    {
        pauseSectionPause.SetActive(true);
        pauseSectionSettings.SetActive(false);

    }
    public void PauseSectionSettings()
    {
        pauseSectionPause.SetActive(false);
        pauseSectionSettings.SetActive(true);
    }

    private void OnDisable()
    {
        pControls.UI.Disable();

    }
}
