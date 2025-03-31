using UnityEngine;
using UnityEngine.InputSystem;

public class ZoomController : MonoBehaviour
{
    public InputActionProperty leftJoystick; // Left joystick input
    public Transform cameraTransform; // Camera for zooming
    public Transform reticle; // Reticle position
    public LayerMask zoomableLayer; // Only zoom when aiming at these objects

    public float zoomSpeed = 2f; // Speed of zoom
    public float minZoom = 0.5f; // Minimum zoom distance
    public float maxZoom = 5f; // Maximum zoom distance
    private float currentZoom = 3f; // Default zoom level

    void Update()
    {
        float zoomInput = leftJoystick.action.ReadValue<Vector2>().y; // Get joystick Y input

        // Check if reticle is on a zoomable object
        if (Physics.Raycast(reticle.position, reticle.forward, out RaycastHit hit, Mathf.Infinity, zoomableLayer))
        {
            if (Mathf.Abs(zoomInput) > 0.1f) // Ensure joystick is actually moved
            {
                // Adjust zoom level
                currentZoom -= zoomInput * zoomSpeed * Time.deltaTime;
                currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

                // Move camera toward or away from reticle's target point
                Vector3 zoomDirection = (hit.point - cameraTransform.position).normalized;
                cameraTransform.position = hit.point - zoomDirection * currentZoom;
            }
        }
    }
}
