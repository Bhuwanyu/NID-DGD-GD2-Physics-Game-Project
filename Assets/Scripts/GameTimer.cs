using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Needed to restart the scene

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 60;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    
    public GameObject gameOverPanel; // Drag your GameOverPanel here

    private void Start()
    {
        timerIsRunning = true;
        if(gameOverPanel != null) gameOverPanel.SetActive(false);
        Time.timeScale = 1f; // Ensure game is running at start
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                GameOver();
            }
        }
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Freezes the game
    }

    // This function will be called by your Restart Button
    public void RestartGame()
    {
        Time.timeScale = 1f; // Must unfreeze time before reloading!
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}