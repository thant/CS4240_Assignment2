using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ReticleController : MonoBehaviour
{
    public Transform leftController; // Left controller transform
    public Transform reticle; // Reticle object
    public float reticleDistance = 1f; // Max distance of reticle
    public LayerMask interactableLayer; // Layer for interactable objects
    
    public InputActionProperty leftJoystickInput; // Left joystick movement
    public InputActionProperty interactButton; // Button for clicking UI

    private Vector2 joystickInput;
    private Vector3 reticleOffset = Vector3.zero;
    public float moveSpeed = 0.5f; // Speed of reticle movement
    public float depthSpeed = 1f; // Speed of reticle depth movement

    void Update()
    {
        // Read joystick input
        joystickInput = leftJoystickInput.action.ReadValue<Vector2>();
        
        // Convert joystick movement to world space direction
        Vector3 moveDirection = (leftController.right * joystickInput.x) * moveSpeed;
        
        // Adjust depth (forward/backward movement in world space)
        float depthMovement = joystickInput.y * depthSpeed * Time.deltaTime;
        reticleDistance = Mathf.Clamp(reticleDistance + depthMovement, 0.1f, 5f);
        
        // Apply movement
        reticleOffset += moveDirection * Time.deltaTime;
        
        // Clamp reticle position within allowed range
        reticleOffset = Vector3.ClampMagnitude(reticleOffset, 1f);
        
        // Set new reticle position
        reticle.position = leftController.position + leftController.forward * reticleDistance + reticleOffset;
        
        // Raycast to check for interactions
        Ray ray = new Ray(reticle.position, leftController.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, reticleDistance, interactableLayer))
        {
            reticle.position = hit.point; // Snap reticle to object
        }
        
        // Check for interact button press
        if (interactButton.action.WasPressedThisFrame())
        {
            TryClickUI(ray);
        }
    }

    void TryClickUI(Ray ray)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Camera.main.WorldToScreenPoint(reticle.position);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        if (results.Count > 0)
        {
            foreach (RaycastResult result in results)
            {
                Button button = result.gameObject.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.Invoke(); // Simulate button click
                    break;
                }
            }
        }
    }
}