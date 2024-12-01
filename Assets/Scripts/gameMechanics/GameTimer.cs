using UnityEngine;
using TMPro; // For TextMeshPro UI
using UnityEngine.SceneManagement; // For loading scenes

public class GameTimer : MonoBehaviour
{
    public float timeLeft = 180f; // 3 minutes = 180 seconds
    public TMP_Text timerText; // Reference to the TextMeshPro Text element for the timer
    public string badEndingScene = "badEnding"; // Name of the bad ending scene

    private bool timerRunning = true;

    void Update()
    {
        if (timerRunning)
        {
            // Count down the time
            timeLeft -= Time.deltaTime;

            // Update the timer text in minutes and seconds
            DisplayTime(timeLeft);

            // Check if time is up
            if (timeLeft <= 0)
            {
                timeLeft = 0;
                timerRunning = false; // Stop the timer
                LoadBadEndingScene(); // Trigger bad ending scene
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        // Convert seconds into minutes and seconds
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Update the UI text element to show the formatted time (using TMP_Text)
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void LoadBadEndingScene()
    {
        // Load the bad ending scene when time is up
        SceneManager.LoadScene(badEndingScene);
    }
}
