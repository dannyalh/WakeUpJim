using UnityEngine;

public class ComputerPuzzleInteraction : MonoBehaviour
{
    public GameObject panel;       // Reference to the puzzle panel
    public KeyCode interactionKey = KeyCode.E; // Key to press to interact
    public AudioSource clickSound; // Reference to the AudioSource for the click sound

    private bool isPlayerInRange = false; // Track if the player is close enough to interact

    private void Start()
    {
        Debug.Log("ComputerPuzzleInteraction script has started."); // Confirm script initialization
    }

    private void Update()
    {
        // Log to confirm Update is running
        Debug.Log("Update is running in ComputerPuzzleInteraction.");

        // Check if the player is in range and presses the interaction key
        if (isPlayerInRange && Input.GetKeyDown(interactionKey))
        {
            PlayClickSound(); // Play the click sound
            ShowPanel();      // Show the puzzle panel
        }
    }

    private void PlayClickSound()
    {
        if (clickSound != null)
        {
            clickSound.Play(); // Play the click sound
            Debug.Log("Click sound played.");
        }
        else
        {
            Debug.LogWarning("Click sound AudioSource is not assigned in the Inspector!");
        }
    }

    private void ShowPanel()
    {
        if (panel != null)
        {
            panel.SetActive(true); // Show the puzzle panel
            Debug.Log("Panel activated.");
        }
        else
        {
            Debug.LogWarning("Panel reference is missing in the Inspector!");
        }
    }

    // Detect when the player enters the interaction range
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Assuming the player has the "Player" tag
        {
            isPlayerInRange = true;
            Debug.Log("Player entered interaction range.");
        }
    }

    // Detect when the player leaves the interaction range
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Player exited interaction range.");
        }
    }
}
