using UnityEngine;

public class PuzzleArrowManager : MonoBehaviour
{
    public GameObject A1; // Arrow for Puzzle 1
    public GameObject A2; // Arrow for Puzzle 2
    public GameObject A3; // Arrow for Puzzle 3
    public GameObject A4; // Arrow for Puzzle 4
    public GameObject A5; // Arrow for the door (final arrow)

    // Method to control arrow visibility based on puzzle progress
    public void ShowArrowForPuzzle(int puzzleIndex)
    {
        // Hide all arrows first
        A1.SetActive(false);
        A2.SetActive(false);
        A3.SetActive(false);
        A4.SetActive(false);
        A5.SetActive(false);

        // Activate the arrow for the specified puzzle
        switch (puzzleIndex)
        {
            case 1:
                A1.SetActive(true);
                break;
            case 2:
                A2.SetActive(true);
                break;
            case 3:
                A3.SetActive(true);
                break;
            case 4:
                A4.SetActive(true);
                break;
            case 5: // Arrow for the door
                A5.SetActive(true);
                break;
        }
    }
}
