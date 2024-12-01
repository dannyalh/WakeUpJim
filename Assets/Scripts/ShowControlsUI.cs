using UnityEngine;
using System.Collections;

public class ShowControlsUI : MonoBehaviour
{
    public float displayTime = 5f;  // Duration the controls panel will be visible

    void Start()
    {
        // Start a coroutine to hide the panel after the specified time
        StartCoroutine(HidePanelAfterDelay());
    }

    private IEnumerator HidePanelAfterDelay()
    {
        yield return new WaitForSeconds(displayTime);  // Wait for the display time
        gameObject.SetActive(false);                   // Hide the controls panel
    }
}
