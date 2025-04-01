using UnityEngine;
using UnityEngine.InputSystem;

public class ZoomController : MonoBehaviour
{
    public InputActionProperty zoomButton; // Button to activate zoom
    public Transform cameraTransform; // Reference point for movement
    public Transform reticle; // Reticle position
    public string zoomableTag = "Zoomable"; // Tag for zoomable objects

    public float zoomSpeed = 2f; // Speed of movement
    public float minDistance = 0.5f; // Minimum distance to player
    public float maxDistance = 5f; // Maximum distance from player

    void Update()
    {
        if (zoomButton.action.IsPressed()) // Check if zoom button is held
        {
            // Check if reticle is colliding with a tagged object
            Collider[] colliders = Physics.OverlapSphere(reticle.position, 0.1f);
            foreach (Collider col in colliders)
            {
                if (col.CompareTag(zoomableTag))
                {
                    Transform targetTransform = col.transform;
                    
                    // Move object toward the player
                    Vector3 moveDirection = (cameraTransform.position - targetTransform.position).normalized;
                    float step = zoomSpeed * Time.deltaTime;
                    targetTransform.position = Vector3.MoveTowards(targetTransform.position, cameraTransform.position, step);
                    
                    // Stop movement if within min distance
                    if (Vector3.Distance(targetTransform.position, cameraTransform.position) < minDistance)
                    {
                        targetTransform.position = cameraTransform.position;
                    }
                    break;
                }
            }
        }
    }
}