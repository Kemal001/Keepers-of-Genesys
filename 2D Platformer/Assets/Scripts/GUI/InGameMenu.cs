using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    private static InGameMenu instance;

    public static bool GameIsPaused = false;

    public GameObject gameUI;
    public GameObject pauseMenuUI;
    public GameObject ps4Controls;
    public GameObject keyboardControls;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("PauseMenu") || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameIsPaused)
            {
                Pause();
            }
            //else
            //{
            //    Pause();
            //}
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        gameUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void PS4Controls()
    {
        pauseMenuUI.SetActive(false);
        ps4Controls.SetActive(true);
    }

    public void KeyboardControls()
    {
        pauseMenuUI.SetActive(false);
        keyboardControls.SetActive(true);
    }

    public void EscapeFromPS4Controls()
    {
        ps4Controls.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void EscapeFromKeyboardControls()
    {
        keyboardControls.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

}
