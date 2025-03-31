using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ReticleController : MonoBehaviour
{
    public Transform leftController; // Left controller transform
    public Transform reticle; // Reticle object
    public float reticleDistance = 5f; // Max distance of reticle
    public LayerMask interactableLayer; // Layer for interactable objects

    public InputActionProperty interactButton; // Button for clicking UI

    void Update()
    {
        // Cast a ray forward from the left controller
        Ray ray = new Ray(leftController.position, leftController.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, reticleDistance, interactableLayer))
        {
            reticle.position = hit.point; // Move reticle to hit point
        }
        else
        {
            reticle.position = leftController.position + leftController.forward * reticleDistance;
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
