using UnityEngine;
using TMPro;

public class FridgePuzzleManager : MonoBehaviour
{
    public PuzzlePiece[] puzzlePieces; // Assign all pieces in Inspector
    public GameObject puzzleCanvas; // The canvas containing the puzzle UI
    public TextMeshProUGUI puzzleCompleteText; // Text to display "Puzzle Complete!"
    public AudioSource completionSound; // AudioSource for the completion sound

    private Quaternion[] initialRotations; // Store initial rotations of puzzle pieces
    private bool isPuzzleComplete = false; // Track if the puzzle is complete

    [SerializeField] private PuzzleArrowManager arrowManager; // Reference to the arrow manager

    void Start()
    {
        // Store initial rotations before scrambling
        StoreInitialRotations();

        ScramblePieces(); // Scramble pieces at the start (only rotation)

        // Hide the "Puzzle Complete!" message at the start
        if (puzzleCompleteText != null)
        {
            puzzleCompleteText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!isPuzzleComplete && CheckPuzzleComplete())
        {
            Debug.Log("Puzzle Complete!");
            OnPuzzleComplete();
        }
    }

    private void ScramblePieces()
    {
        foreach (PuzzlePiece piece in puzzlePieces)
        {
            int randomRotations = Random.Range(0, 4); // Rotate each piece randomly (90-degree increments)
            piece.transform.Rotate(0, 0, randomRotations * 90);
        }
    }

    private bool CheckPuzzleComplete()
    {
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            if (Quaternion.Angle(puzzlePieces[i].transform.rotation, initialRotations[i]) > 1f)
            {
                return false; // If any piece is not in the correct rotation, the puzzle is incomplete
            }
        }
        return true; // All pieces are in the correct rotations
    }

    private void OnPuzzleComplete()
    {
        isPuzzleComplete = true;

        // Show "Puzzle Complete!" message
        if (puzzleCompleteText != null)
        {
            puzzleCompleteText.gameObject.SetActive(true);
        }

        // Play the completion sound
        PlayCompletionSound();

        // Close the puzzle canvas after a short delay
        Invoke("ClosePuzzleCanvas", 2f);
    }

    private void PlayCompletionSound()
    {
        if (completionSound != null)
        {
            completionSound.Play();
            Debug.Log("Completion sound played.");
        }
        else
        {
            Debug.LogWarning("Completion sound AudioSource is not assigned!");
        }
    }

    private void ClosePuzzleCanvas()
    {
        if (puzzleCanvas != null)
        {
            puzzleCanvas.SetActive(false); // Deactivate the puzzle canvas
        }

        if (puzzleCompleteText != null)
        {
            puzzleCompleteText.gameObject.SetActive(false); // Hide the text
        }

        // Notify arrow manager
        arrowManager.ShowArrowForPuzzle(4); // Show arrow for Puzzle 4
    }

    private void StoreInitialRotations()
    {
        initialRotations = new Quaternion[puzzlePieces.Length];
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            initialRotations[i] = puzzlePieces[i].transform.rotation;
        }
    }

    public void ResetPuzzle()
    {
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            puzzlePieces[i].transform.rotation = initialRotations[i]; // Reset rotation
        }
        isPuzzleComplete = false;
        ScramblePieces();
    }
}
