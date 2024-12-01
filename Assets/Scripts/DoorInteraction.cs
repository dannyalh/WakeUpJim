using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Optional: For showing UI prompts

public class DoorInteraction : MonoBehaviour
{
    public string goodEndingSceneName = "goodEnding"; // Name of the Good Ending scene
    public TextMeshProUGUI interactionPrompt; // Optional: UI Text for "Press E to Enter"
    public AudioSource achievementSound; // Reference to the AudioSource for the achievement sound
    private bool isPlayerNear = false; // Track if the player is near the door

    private void Start()
    {
        // Ensure the interaction prompt is hidden initially
        if (interactionPrompt != null)
        {
            interactionPrompt.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Check if the player presses E while near the door
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            PlayAchievementSound(); // Play the achievement sound
            Invoke(nameof(LoadGoodEndingScene), 0.5f); // Slight delay to let the sound play
        }
    }

    private void PlayAchievementSound()
    {
        if (achievementSound != null)
        {
            achievementSound.Play(); // Play the sound
            Debug.Log("Achievement sound played.");
        }
        else
        {
            Debug.LogWarning("Achievement sound AudioSource is not assigned!");
        }
    }

    private void LoadGoodEndingScene()
    {
        Debug.Log("Loading Good Ending...");
        SceneManager.LoadScene(goodEndingSceneName); // Load the Good Ending scene
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters the trigger area
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = true;

            // Show the interaction prompt
            if (interactionPrompt != null)
            {
                interactionPrompt.text = "Press E to Enter";
                interactionPrompt.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the player leaves the trigger area
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = false;

            // Hide the interaction prompt
            if (interactionPrompt != null)
            {
                interactionPrompt.gameObject.SetActive(false);
            }
        }
    }
}
