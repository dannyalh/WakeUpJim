using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldTransition : MonoBehaviour
{
    public string dreamWorldSceneName = "DreamWorldScene";  // Scene to load
    public GameObject arrowIndicator;  // Arrow to guide player
    public GameObject pressEText;      // Text to prompt player to press E
    private bool isPlayerInTrigger = false;  // Tracks if the player is in the trigger area

    void Start()
    {
        // Ensure arrow and text are initially disabled
        if (arrowIndicator != null) arrowIndicator.SetActive(false);
        if (pressEText != null) pressEText.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player enters the trigger area
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            // Show the arrow and text prompt
            if (arrowIndicator != null) arrowIndicator.SetActive(true);
            if (pressEText != null) pressEText.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player exits the trigger area
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            // Hide the arrow and text prompt
            if (arrowIndicator != null) arrowIndicator.SetActive(false);
            if (pressEText != null) pressEText.SetActive(false);
        }
    }

    void Update()
    {
        // Check if player is in trigger area and presses E
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            // Load the dream world scene
            SceneManager.LoadScene(dreamWorldSceneName);
        }
    }
}
