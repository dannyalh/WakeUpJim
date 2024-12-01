using UnityEngine;
using UnityEngine.SceneManagement;

public class GoodEndingController : MonoBehaviour
{
    public void NavigateToMainMenu()
    {
        // Load the MainMenu scene
        SceneManager.LoadScene("MainMenu");
    }
}
