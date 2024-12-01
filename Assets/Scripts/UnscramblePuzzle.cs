using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class WordScramblePuzzle : MonoBehaviour
{
    public TextMeshProUGUI scrambleText;
    public GameObject buttonContainer;
    public TextMeshProUGUI feedbackText;
    public Button letterButtonPrefab;
    public Button submitButton;
    public GameObject playerInputPrefab;
    public GameObject inputContainer;
    public GameObject puzzleCanvas;

    private List<string> words = new List<string> { "NIGHT", "SLEEP", "GUARD", "SHIFT", "WAKEUP" };
    private string currentWord;
    private string scrambledWord;
    private List<Button> letterButtons = new List<Button>();
    private GameObject currentPlayerInput;

    [SerializeField] private PuzzleArrowManager arrowManager; // Reference to arrow manager
    public AudioSource completionSound; // AudioSource for the completion sound

    void Start()
    {
        feedbackText.text = "";
        submitButton.onClick.AddListener(CheckAnswer);
        LoadNextWord();
    }

    void LoadNextWord()
    {
        if (words.Count == 0)
        {
            feedbackText.text = "Puzzle Complete!";
            PlayCompletionSound(); // Play the completion sound
            Invoke("ClosePuzzleCanvas", 2f);
            return;
        }

        currentWord = words[0];
        words.RemoveAt(0);
        scrambledWord = ScrambleWord(currentWord);
        scrambleText.text = scrambledWord;

        SetupLetterButtons();
        SetupPlayerInput();
    }

    string ScrambleWord(string word)
    {
        List<char> letters = new List<char>(word);
        System.Random rand = new System.Random();

        for (int i = 0; i < letters.Count; i++)
        {
            int randomIndex = rand.Next(letters.Count);
            char temp = letters[i];
            letters[i] = letters[randomIndex];
            letters[randomIndex] = temp;
        }

        return new string(letters.ToArray());
    }

    void SetupLetterButtons()
    {
        foreach (Transform child in buttonContainer.transform)
        {
            Destroy(child.gameObject);
        }

        letterButtons.Clear();

        for (int i = 0; i < scrambledWord.Length; i++)
        {
            Button newButton = Instantiate(letterButtonPrefab, buttonContainer.transform);
            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = scrambledWord[i].ToString();

            int index = i;
            newButton.onClick.RemoveAllListeners();
            newButton.onClick.AddListener(() => OnLetterButtonClick(index));

            letterButtons.Add(newButton);
        }
    }

    void OnLetterButtonClick(int index)
    {
        if (currentPlayerInput == null) return;

        TMP_InputField inputField = currentPlayerInput.GetComponent<TMP_InputField>();
        if (inputField != null)
        {
            inputField.text += letterButtons[index].GetComponentInChildren<TextMeshProUGUI>().text;
        }
    }

    void SetupPlayerInput()
    {
        if (currentPlayerInput != null)
        {
            Destroy(currentPlayerInput);
        }

        currentPlayerInput = Instantiate(playerInputPrefab, inputContainer.transform);

        TMP_InputField inputField = currentPlayerInput.GetComponent<TMP_InputField>();
        if (inputField != null)
        {
            inputField.text = "";
        }
    }

    void CheckAnswer()
    {
        if (currentPlayerInput == null) return;

        TMP_InputField inputField = currentPlayerInput.GetComponent<TMP_InputField>();

        if (inputField.text.Equals(currentWord, System.StringComparison.OrdinalIgnoreCase))
        {
            feedbackText.text = "Correct!";
            Invoke("LoadNextWord", 1f);
        }
        else
        {
            feedbackText.text = "Try Again!";
            inputField.text = "";
        }
    }

    void ClosePuzzleCanvas()
    {
        if (puzzleCanvas != null)
        {
            puzzleCanvas.SetActive(false);
        }

        // Notify arrow manager
        arrowManager.ShowArrowForPuzzle(3); // Show arrow for Puzzle 3
    }

    void PlayCompletionSound()
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
}
