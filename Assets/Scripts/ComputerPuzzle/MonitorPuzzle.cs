using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonitorPuzzle : MonoBehaviour
{
    public Button[] panels;                   // Array of panels (buttons) to interact with
    private int currentPanelIndex = 0;        // The current panel to be clicked
    private bool isPuzzleComplete = false;    // Track if the puzzle is completed

    public Color activeColor = Color.green;   // Color when a panel is clicked correctly
    public Color defaultColor = Color.white;  // Default panel color

    public GameObject monitorPuzzleCanvas;    // Canvas for the monitor puzzle
    public TextMeshProUGUI puzzleCompleteText; // TMP for "Puzzle Complete!" message
    public float popupDisplayTime = 2f;       // Duration the "Puzzle Complete" text is displayed

    public AudioSource completionSound;       // AudioSource for the completion sound

    [SerializeField] private PuzzleArrowManager arrowManager; // Reference to the arrow manager
    private int[] randomizedSequence;         // Sequence of panel indices for correct order

    void Start()
    {
        foreach (var panel in panels)
        {
            panel.GetComponent<Image>().color = defaultColor;
            panel.onClick.AddListener(() => OnPanelClick(panel));
        }

        RandomizePanelOrder();

        if (puzzleCompleteText != null)
        {
            puzzleCompleteText.gameObject.SetActive(false);
        }
    }

    void RandomizePanelOrder()
    {
        randomizedSequence = new int[panels.Length];
        for (int i = 0; i < panels.Length; i++)
        {
            randomizedSequence[i] = i;
        }

        for (int i = 0; i < randomizedSequence.Length; i++)
        {
            int temp = randomizedSequence[i];
            int randomIndex = Random.Range(i, randomizedSequence.Length);
            randomizedSequence[i] = randomizedSequence[randomIndex];
            randomizedSequence[randomIndex] = temp;
        }

        currentPanelIndex = 0;
        isPuzzleComplete = false;
    }

    void OnPanelClick(Button clickedPanel)
    {
        if (isPuzzleComplete) return;

        int clickedIndex = System.Array.IndexOf(panels, clickedPanel);

        if (clickedIndex == randomizedSequence[currentPanelIndex])
        {
            clickedPanel.GetComponent<Image>().color = activeColor;
            currentPanelIndex++;

            if (currentPanelIndex == panels.Length)
            {
                isPuzzleComplete = true;
                Debug.Log("Puzzle Complete!");
                PuzzleCompleted();
            }
        }
        else
        {
            ResetPanels();
        }
    }

    void ResetPanels()
    {
        foreach (var panel in panels)
        {
            panel.GetComponent<Image>().color = defaultColor;
        }
        currentPanelIndex = 0;
    }

    void PuzzleCompleted()
    {
        Debug.Log("PuzzleCompleted method called.");
        
        // Play the completion sound
        PlayCompletionSound();

        if (puzzleCompleteText != null)
        {
            puzzleCompleteText.gameObject.SetActive(true);
        }
        Invoke("CloseCanvas", popupDisplayTime);
    }

    void PlayCompletionSound()
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

    void CloseCanvas()
    {
        if (monitorPuzzleCanvas != null)
        {
            monitorPuzzleCanvas.SetActive(false);
        }

        if (puzzleCompleteText != null)
        {
            puzzleCompleteText.gameObject.SetActive(false);
        }

        // Notify arrow manager
        arrowManager.ShowArrowForPuzzle(2); // Show arrow for Puzzle 2
    }
}
