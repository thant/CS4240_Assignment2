using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectRotator : MonoBehaviour
{
    public InputActionProperty rightJoystick; // Right joystick action
    public float rotationSpeed = 100f; // Speed of rotation

    void Update()
    {
        // Get the joystick input
        Vector2 joystickInput = rightJoystick.action.ReadValue<Vector2>();

        // Rotate based on joystick input
        float rotateY = joystickInput.x * rotationSpeed * Time.deltaTime;
        float rotateX = -joystickInput.y * rotationSpeed * Time.deltaTime; // Optional for up/down tilt

        // Apply rotation
        transform.Rotate(Vector3.up, rotateY, Space.World);
        transform.Rotate(Vector3.right, rotateX, Space.Self); // Optional tilt
    }
}
