using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // assign the PauseMenu panel
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // resume game time
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked; // lock cursor again
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // pause game time
        isPaused = true;
        Cursor.lockState = CursorLockMode.None; // unlock cursor
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); // logs in editor
        Application.Quit();     // quits in build
    }

    public void OpenOptions()
    {
        Debug.Log("Options Menu Opened"); 
        // Here you can open an options panel or scene
    }
}

