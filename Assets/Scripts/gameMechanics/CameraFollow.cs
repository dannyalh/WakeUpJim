using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;      // Reference to Jim's transform
    public float smoothSpeed = 0.125f;  // Speed of camera smoothing
    public Vector3 offset;        // Offset from the target position

    void LateUpdate()
    {
        if (target == null) return;

        // Desired camera position based on Jim's position + offset
        Vector3 desiredPosition = target.position + offset;
        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
