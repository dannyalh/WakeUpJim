using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public GameObject puzzleCanvas; // Assign the puzzle canvas in the Inspector
    public KeyCode activationKey = KeyCode.E; // Key to activate the puzzle
    public AudioSource clickSound; // Reference to the AudioSource for the click sound
    private bool isPlayerInTrigger = false; // Tracks if the player is in the trigger zone

    void Start()
    {
        // Ensure the puzzle canvas is initially inactive
        if (puzzleCanvas != null)
            puzzleCanvas.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player enters the trigger zone
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            Debug.Log("Player entered the puzzle trigger zone!");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player exits the trigger zone
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            Debug.Log("Player left the puzzle trigger zone!");
        }
    }

    void Update()
    {
        // Activate the puzzle when the player presses the activation key
        if (isPlayerInTrigger && Input.GetKeyDown(activationKey))
        {
            PlayClickSound(); // Play the click sound
            ActivatePuzzle(); // Activate the puzzle canvas
        }
    }

    void PlayClickSound()
    {
        if (clickSound != null)
        {
            clickSound.Play(); // Play the click sound
            Debug.Log("Click sound played.");
        }
        else
        {
            Debug.LogWarning("Click sound AudioSource is not assigned!");
        }
    }

    void ActivatePuzzle()
    {
        if (puzzleCanvas != null)
        {
            puzzleCanvas.SetActive(true); // Show the puzzle canvas
            Debug.Log("Puzzle triggered!");
        }
        else
        {
            Debug.LogWarning("Puzzle canvas not assigned!");
        }
    }
}
