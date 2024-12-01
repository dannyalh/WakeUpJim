using UnityEngine;
using UnityEngine.SceneManagement;

public class DreamCutsceneTransition : MonoBehaviour
{
    public string dreamCutsceneSceneName = "dreamCutscene";  // Scene to load
    public GameObject pressEText;      // Text to prompt player to press E
    public AudioSource clickSound;     // Reference to the AudioSource for the click sound
    private bool isPlayerInTrigger = false;  // Tracks if the player is in the trigger area

    void Start()
    {
        // Ensure the text is initially disabled
        if (pressEText != null) pressEText.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player enters the trigger area
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            // Show the text prompt
            if (pressEText != null) pressEText.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player exits the trigger area
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            // Hide the text prompt
            if (pressEText != null) pressEText.SetActive(false);
        }
    }

    void Update()
    {
        // Check if player is in trigger area and presses E
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            PlayClickSound(); // Play the click sound
            Invoke(nameof(LoadDreamCutscene), 0.5f); // Delay scene load to let the sound play
        }
    }

    private void PlayClickSound()
    {
        if (clickSound != null)
        {
            clickSound.Play();
        }
        else
        {
            Debug.LogWarning("Click sound AudioSource is not assigned in the Inspector!");
        }
    }

    private void LoadDreamCutscene()
    {
        // Load the dream cutscene scene
        SceneManager.LoadScene(dreamCutsceneSceneName);
    }
}
