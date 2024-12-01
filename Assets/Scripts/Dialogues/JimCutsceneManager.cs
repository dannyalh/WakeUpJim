using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class JimCutsceneManager : MonoBehaviour
{
    [Header("Cutscene Settings")]
    public string dreamWorldSceneName = "DreamWorldScene"; // Name of the dream world scene
    public TextMeshProUGUI dialogText; // Text component for dialog display
    public float textSpeed = 0.05f; // Speed of text display

    [Header("Dialog Content")]
    [TextArea(2, 5)]
    public string[] dialogLines; // Lines of dialog Jim will say

    private int currentLineIndex = 0; // Tracks the current dialog line
    private bool isDisplayingText = false; // Tracks if text is being displayed

    void Start()
    {
        if (dialogText != null)
        {
            dialogText.text = ""; // Clear the dialog box at the start
            StartCutscene();
        }
        else
        {
            Debug.LogError("Dialog Text is not assigned in the Inspector!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDisplayingText) // Left click (mouse button 0)
        {
            // Show the next line or transition to the dream world
            if (currentLineIndex < dialogLines.Length)
            {
                StartCoroutine(DisplayText(dialogLines[currentLineIndex]));
                currentLineIndex++;
            }
            else
            {
                TransitionToDreamWorld();
            }
        }
    }

    void StartCutscene()
    {
        if (dialogLines.Length > 0)
        {
            StartCoroutine(DisplayText(dialogLines[currentLineIndex]));
            currentLineIndex++;
        }
        else
        {
            Debug.LogWarning("No dialog lines assigned!");
        }
    }

    System.Collections.IEnumerator DisplayText(string line)
    {
        isDisplayingText = true;
        dialogText.text = ""; // Clear the dialog box

        foreach (char letter in line)
        {
            dialogText.text += letter; // Display one letter at a time
            yield return new WaitForSeconds(textSpeed);
        }

        isDisplayingText = false; // Allow skipping to the next line
    }

    void TransitionToDreamWorld()
    {
        SceneManager.LoadScene(dreamWorldSceneName); // Load the dream world scene
    }
}
