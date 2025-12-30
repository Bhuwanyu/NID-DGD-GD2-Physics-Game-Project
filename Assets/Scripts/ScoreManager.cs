using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int scoreValue = 0;
    public TextMeshProUGUI scoreText;
    
    public int totalHexagons; // Set this in the inspector
    public GameObject winPanel; // Drag your WinPanel here

    void Start()
    {
        scoreValue = 0;
        // Automatically count how many hexagons are in the scene at the start
        totalHexagons = GameObject.FindGameObjectsWithTag("Hexagon").Length;
        
        if(winPanel != null)
            winPanel.SetActive(false);
    }

    void Update()
    {
        scoreText.text = "Score: " + scoreValue;

        // Check if all hexagons are collected
        if (scoreValue >= totalHexagons && totalHexagons > 0)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        winPanel.SetActive(true);
        // Optional: Stop time so the timer/game freezes
        Time.timeScale = 0f; 
    }
}