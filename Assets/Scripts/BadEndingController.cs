using UnityEngine;
using UnityEngine.SceneManagement;

public class BadEndingController : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu"; // Name of the Main Menu scene

    // This method is called when the Main Menu button is clicked
    public void NavigateToMainMenu()
    {
        // Check if the scene exists in the build settings
        if (IsSceneInBuildSettings(mainMenuSceneName))
        {
            SceneManager.LoadScene(mainMenuSceneName); // Load the Main Menu scene
        }
        else
        {
            Debug.LogError("Scene not found in build settings: " + mainMenuSceneName);
        }
    }

    // Helper method to check if a scene is in the build settings
    private bool IsSceneInBuildSettings(string sceneName)
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
}
