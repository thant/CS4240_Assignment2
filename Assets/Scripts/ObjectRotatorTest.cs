using UnityEngine;

public class ObjectRotatorTest : MonoBehaviour
{
    public float rotationSpeed = 100f; // Speed of rotation
    private bool isRotating = false; // Whether the user is currently rotating the object

    private Vector3 lastMousePosition; // Last mouse position to calculate delta

    void Update()
    {
        // When the left mouse button is pressed down, start rotating
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            isRotating = true;
            lastMousePosition = Input.mousePosition; // Store the initial mouse position
        }

        // When the left mouse button is released, stop rotating
        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        // If the mouse is being held and moved, rotate the object
        if (isRotating)
        {
            // Get the current mouse position
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;

            // Rotate the object based on the mouse movement (scaled by rotation speed)
            float mouseX = mouseDelta.x;
            float mouseY = mouseDelta.y;

            // Apply rotation to the object
            transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.deltaTime, Space.World); // Rotate around Y axis (left-right)
            transform.Rotate(Vector3.right, -mouseY * rotationSpeed * Time.deltaTime, Space.Self); // Rotate around X axis (up-down)

            // Update the last mouse position for the next frame
            lastMousePosition = Input.mousePosition;
        }

        // Keep the object at its initial position (this should already be the case, but it's good to ensure it)
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}
