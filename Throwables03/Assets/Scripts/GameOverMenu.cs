using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject gameOverPanel;
    public CanvasGroup canvasGroup; // attach CanvasGroup from panel
    public Text scoreText;          // final score text
    public Text waveText;           // final wave text
    public float fadeDuration = 1f; // fade in time

    void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
            if (canvasGroup != null) canvasGroup.alpha = 0f;
        }
    }

    public void ShowGameOver(int finalScore = 0, int finalWave = 0)
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // Update score and wave
        if (scoreText != null)
            scoreText.text = "Score: " + finalScore;

        if (waveText != null)
            waveText.text = "Wave: " + finalWave;

        // Pause the game
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Start fade-in
        if (canvasGroup != null)
            StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            elapsed += Time.unscaledDeltaTime; // use unscaled time because Time.timeScale = 0
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // replace with your scene name
    }
}
