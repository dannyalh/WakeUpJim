using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

public class KeycardClick : MonoBehaviour, IPointerClickHandler
{
    public Canvas puzzleCanvas; // Canvas to close after puzzle completion
    public TextMeshProUGUI puzzleCompleteText; // Display "Puzzle Complete!"
    public float delayBeforeClosing = 2f; // Time delay before closing the canvas
    public float notificationDuration = 5f; // Time the notification text is visible

    [SerializeField] private PuzzleArrowManager arrowManager; // Reference to the arrow manager
    public AudioSource completionSound; // AudioSource for the completion sound

    // Triggered when the keycard is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsInCorrectPosition())
        {
            // Play the completion sound
            PlayCompletionSound();

            // Show "The card is secured, Jim wake up!" text
            if (puzzleCompleteText != null)
            {
                puzzleCompleteText.text = "The card is secured, Jim wake up!";
                puzzleCompleteText.gameObject.SetActive(true);

                // Hide the text after the notification duration
                StartCoroutine(HideNotificationAfterDelay(notificationDuration));
            }

            // Start the closing and arrow logic
            StartCoroutine(CloseCanvasAfterDelay());
        }
    }

    private void PlayCompletionSound()
    {
        if (completionSound != null)
        {
            completionSound.Play(); // Play the sound
            Debug.Log("Completion sound played.");
        }
        else
        {
            Debug.LogWarning("Completion sound AudioSource is not assigned!");
        }
    }

    private IEnumerator HideNotificationAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);

        // Hide the notification text
        if (puzzleCompleteText != null)
        {
            puzzleCompleteText.gameObject.SetActive(false);
        }
    }

    private IEnumerator CloseCanvasAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeClosing);

        // Close the puzzle canvas
        if (puzzleCanvas != null)
        {
            puzzleCanvas.gameObject.SetActive(false);
        }

        // Notify the arrow manager to show Arrow 5
        arrowManager.ShowArrowForPuzzle(5);
    }

    private bool IsInCorrectPosition()
    {
        // Placeholder for checking if the keycard is placed correctly
        return true; // Update this logic as needed
    }
}
