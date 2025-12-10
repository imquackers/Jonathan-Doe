using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
    // This function is called when Start Game button is clicked
    public void StartGame()
    {
        // Load your main game scene by name or build index
        SceneManager.LoadScene("Game"); // replace "GameScene" with your scene name
    }

    // This function is called when Quit button is clicked
    public void QuitGame()
    {
        Debug.Log("Quit Game"); // logs in editor
        Application.Quit();     // quits in build
    }
}

