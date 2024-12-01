using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    private float correctRotation; // The original correct rotation

    void Start()
    {
        correctRotation = transform.eulerAngles.z; // Save the correct rotation at start
    }

    void OnMouseDown()
    {
        RotatePiece(); // Rotate the piece on click
    }

    private void RotatePiece()
    {
        // Rotate the piece 90 degrees clockwise
        transform.Rotate(0, 0, -90);
    }

    public bool IsAligned()
    {
        // Check if the piece's rotation matches the correct rotation
        float currentRotation = transform.eulerAngles.z % 360;
        float targetRotation = correctRotation % 360;

        return Mathf.Approximately(currentRotation, targetRotation);
    }
}
