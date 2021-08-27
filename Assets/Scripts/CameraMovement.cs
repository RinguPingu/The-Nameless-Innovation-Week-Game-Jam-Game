using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform target;
    public float cameraSpeed = 0.1f;
    public Vector3 offset;

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, cameraSpeed);
        transform.position = smoothedPosition;
    }
}
