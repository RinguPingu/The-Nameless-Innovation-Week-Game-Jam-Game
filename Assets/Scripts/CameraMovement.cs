using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{

    public Transform target;
    public float cameraSpeed = 0.1f;
    public Vector3 offset;

    [SerializeField]
    SpriteRenderer clampToBounds;

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, cameraSpeed);

        // clamp cam pos
        var cam = GetComponent<Camera>();
        var viewHalfHeight = cam.orthographicSize;
        var viewHalfWidth = viewHalfHeight * cam.aspect;
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, clampToBounds.bounds.min.x + viewHalfWidth, clampToBounds.bounds.max.x - viewHalfWidth);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, clampToBounds.bounds.min.y + viewHalfHeight, clampToBounds.bounds.max.y - viewHalfHeight);

        transform.position = smoothedPosition;
    }
}
