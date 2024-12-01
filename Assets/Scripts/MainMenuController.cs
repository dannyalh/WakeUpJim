using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public Button playButton;  // Reference to your existing Play button in the Canvas
    public string cutsceneSceneName = "CutsceneScene";  // Name of the scene you want to load
    public Image fadeImage;  // Reference to the Image (black screen) for fading effect
    public float fadeDuration = 1f;  // Duration for the fade effect

    void Start()
    {
        // Add the Play button functionality
        if (playButton != null)
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
        }
        else
        {
            Debug.LogError("Play button is not assigned in the Inspector!");
        }
        
        // Ensure the fade image starts as transparent
        if (fadeImage != null)
        {
            fadeImage.color = new Color(0, 0, 0, 0);  // Set the initial color to fully transparent
        }
        else
        {
            Debug.LogError("Fade Image is not assigned in the Inspector!");
        }
    }

    // This method will be called when the Play button is clicked
    void OnPlayButtonClicked()
    {
        // Check if the scene exists in the build settings before trying to load it
        if (IsSceneInBuildSettings(cutsceneSceneName))
        {
            StartCoroutine(FadeAndLoadScene(cutsceneSceneName));  // Start fade effect and then load the next scene
        }
        else
        {
            Debug.LogError("Scene not found in build settings: " + cutsceneSceneName);
        }
    }

    // Helper method to check if the scene is in the build settings
    bool IsSceneInBuildSettings(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneFileName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneFileName == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    // Coroutine to handle fading out and loading the next scene
    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        // Fade to black
        float timeElapsed = 0f;
        while (timeElapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, timeElapsed / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 1); // Ensure it's fully opaque

        // Load the scene after fading
        SceneManager.LoadScene(sceneName);
    }
}
