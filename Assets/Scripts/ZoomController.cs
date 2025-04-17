using UnityEngine;
using UnityEngine.InputSystem;

public class ZoomController : MonoBehaviour
{
    public InputActionProperty joystickInput;           // Vector2 (e.g., left/right thumbstick)
    public Transform cameraTransform;
    public string zoomableTag = "Zoomable";

    public float zoomSpeed = 2f;
    public float minDistance = 0.5f;
    public float maxDistance = 5f;
    public float inputThreshold = 0.1f;  // To avoid jitter from small joystick movement

    void Update()
    {
        float vertical = joystickInput.action.ReadValue<Vector2>().y;

        // Ignore small values to prevent unintentional zooming
        if (Mathf.Abs(vertical) > inputThreshold)
        {
            // Find all active objects with the "Zoomable" tag
            GameObject[] zoomables = GameObject.FindGameObjectsWithTag(zoomableTag);

            foreach (GameObject obj in zoomables)
            {
                Transform target = obj.transform;
                float distance = Vector3.Distance(target.position, cameraTransform.position);

                // Decide direction based on joystick input
                int direction = vertical > 0 ? 1 : -1;

                // Only move if within bounds
                if ((direction == -1 && distance < maxDistance) || (direction == 1 && distance > minDistance))
                {
                    Vector3 moveDir = (cameraTransform.position - target.position).normalized;
                    float step = zoomSpeed * Time.deltaTime * direction;
                    target.position = Vector3.MoveTowards(target.position, cameraTransform.position, -step);
                }
            }
        }
    }
}
