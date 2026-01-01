using UnityEngine;
using UnityEngine.SceneManagement; // Required for switching scenes

public class MainMenu : MonoBehaviour
{
    public GameObject controlsPanel;

    public void PlayGame()
    {
        // Loads the next scene in the build queue
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Button Pressed!"); // Confirms it works in the editor
        Application.Quit(); // Closes the actual app
    }

    // Function to show the panel
    public void OpenControls()
    {
        if (controlsPanel != null)
        {
            controlsPanel.SetActive(true);
        }
    }

    // Function to hide the panel
    public void CloseControls()
    {
        if (controlsPanel != null)
        {
            controlsPanel.SetActive(false);
        }
    }
}