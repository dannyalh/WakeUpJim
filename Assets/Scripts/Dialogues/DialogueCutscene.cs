using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class DialogueCutscene : MonoBehaviour
{
    public Image portraitA; // Jim's portrait
    public Image portraitB; // Boss's portrait
    public TextMeshProUGUI nameA; // Jim's name
    public TextMeshProUGUI nameB; // Boss's name
    public TextMeshProUGUI dialogueText; // Dialogue text
    public Image fadeOverlay; // Fullscreen image for fading effect
    public float fadeDuration = 0.5f; // Duration for fade in/out

    private int currentDialogueIndex = 0;
    private bool isDialogueActive = false;

    private string[] dialogueLines = {
        "Jim: Good afternoon sir",
        "Boss: Listen here, Jim is it?",
        "Jim: Yes sir",
        "Boss: As the new night shift security guard here it's your job to be the eyes and ears of the company.",
        "Boss: Don't even blink when you look at that monitor or else we'll have problems.", 
        "Boss: You mess up and you're out of here. Understand?",
        "Jim: Of course sir I'll head to my office...",
        "Jim: If you can show me where that is please.",
        "Boss: Just head to the top left corner office."
    };

    void Start()
    {
        // Initialize dialogue
        nameA.text = "Jim";
        nameB.text = "";
        fadeOverlay.color = new Color(0, 0, 0, 0); // Ensure fade overlay starts transparent
        ShowDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isDialogueActive)
        {
            NextDialogue();
        }
    }

    private void ShowDialogue()
    {
        if (currentDialogueIndex < dialogueLines.Length)
        {
            string dialogue = dialogueLines[currentDialogueIndex];
            string speakerName = GetSpeakerName(dialogue);
            string message = GetDialogueText(dialogue);

            dialogueText.text = message;

            if (speakerName == "Jim")
            {
                nameA.text = "Jim";
                nameB.text = "";
                StartCoroutine(FadeIn(portraitA));
                StartCoroutine(FadeOut(portraitB));
            }
            else
            {
                nameB.text = "Boss";
                nameA.text = "";
                StartCoroutine(FadeIn(portraitB));
                StartCoroutine(FadeOut(portraitA));
            }

            isDialogueActive = true;
        }
        else
        {
            StartCoroutine(EndCutscene());
        }
    }

    private string GetSpeakerName(string dialogue)
    {
        // Split dialogue at the first colon to get the speaker's name
        string speakerName = dialogue.Split(':')[0].Trim();
        return speakerName;
    }

    private string GetDialogueText(string dialogue)
    {
        // Get everything after the colon
        int colonIndex = dialogue.IndexOf(":") + 1;
        return dialogue.Substring(colonIndex).Trim();
    }

    private void NextDialogue()
    {
        if (currentDialogueIndex < dialogueLines.Length - 1)
        {
            currentDialogueIndex++;
            ShowDialogue();
        }
        else
        {
            StartCoroutine(EndCutscene());
        }
    }

    private IEnumerator EndCutscene()
    {
        isDialogueActive = false;
        yield return StartCoroutine(FadeToBlack()); // Fade to black before transitioning
        SceneManager.LoadScene("Wake Up, Jim!"); // Load the next scene
    }

    private IEnumerator FadeOut(Image portrait)
    {
        float elapsedTime = 0;
        Color originalColor = portrait.color;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            portrait.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        portrait.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0); // Fully transparent
    }

    private IEnumerator FadeIn(Image portrait)
    {
        float elapsedTime = 0;
        Color originalColor = portrait.color;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            portrait.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        portrait.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1); // Fully opaque
    }

    private IEnumerator FadeToBlack()
    {
        float elapsedTime = 0;
        Color originalColor = fadeOverlay.color;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            fadeOverlay.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeOverlay.color = new Color(0, 0, 0, 1); // Fully black
    }
}
