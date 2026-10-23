using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Map Boundaries")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    [Header("Settings")]
    public float smoothSpeed = 0.125f;
    private Vector3 offset = new Vector3(0f, 0f, -80f); // Matches your camera Z position

    private void LateUpdate()
    {
        if (player == null) return;

        // Calculate target camera position
        Vector3 desiredPosition = player.position + offset;

        // Clamp positions to stay inside map limits
        float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);

        Vector3 clampedPosition = new Vector3(clampedX, clampedY, desiredPosition.z);

        // Smoothly move the camera
        transform.position = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed);
    }
}