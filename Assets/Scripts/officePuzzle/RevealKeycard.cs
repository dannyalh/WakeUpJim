using UnityEngine;

public class RevealKeycard : MonoBehaviour
{
    public GameObject keycard;

    private bool isMoved = false;

    private void Update()
    {
        if (!isMoved && transform.position != transform.localPosition)
        {
            isMoved = true;
            keycard.SetActive(true);
        }
    }
}
