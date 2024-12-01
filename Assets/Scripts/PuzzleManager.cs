using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject arrowP1;
    public GameObject arrowP2;
    public GameObject monitorPuzzle;
    public GameObject puzzle1CompletePanel;

    public float popupDisplayTime = 2f;

    void Start()
    {
        if (arrowP1 != null) arrowP1.SetActive(true);
        if (arrowP2 != null) arrowP2.SetActive(false);
        if (puzzle1CompletePanel != null) puzzle1CompletePanel.SetActive(false);
        if (monitorPuzzle != null) monitorPuzzle.SetActive(false);
    }

    public void StartPuzzle1()
    {
        if (monitorPuzzle != null)
        {
            monitorPuzzle.SetActive(true);  // Show monitor puzzle
            if (arrowP1 != null) arrowP1.SetActive(false);  // Hide ArrowP1
        }
    }

    public void CompletePuzzle1()
    {
        if (monitorPuzzle != null) monitorPuzzle.SetActive(false);
        if (puzzle1CompletePanel != null) puzzle1CompletePanel.SetActive(true);

        Invoke("ShowArrowP2", popupDisplayTime);
    }

    void ShowArrowP2()
    {
        if (puzzle1CompletePanel != null) puzzle1CompletePanel.SetActive(false);
        if (arrowP2 != null) arrowP2.SetActive(true);
    }
}
