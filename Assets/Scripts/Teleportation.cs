using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class Teleportation : MonoBehaviour
{
    public InputActionReference teleportAction; // Action to initiate teleportation
    public GameObject teleportIndicatorPrefab; // Prefab for the teleport indicator
    private GameObject teleportIndicator; // Instance of the teleport indicator
    public LayerMask teleportMask; // Layer mask to define valid teleportation surfaces
    public Transform player; // Reference to the player (XR Rig) transform

    private void Start()
    {
        teleportIndicator = Instantiate(teleportIndicatorPrefab);
        teleportIndicator.SetActive(false); // Disable the indicator initially

        if (teleportAction == null || player == null)
        {
            Debug.LogError("Teleport Action or Player transform is not set!");
            return;
        }

        teleportAction.action.Enable();
        teleportAction.action.performed += OnTeleportActionPerformed;
        teleportAction.action.canceled += OnTeleportActionCanceled;
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f, teleportMask))
        {
            teleportIndicator.transform.position = hit.point; // Update indicator position
            if (!teleportIndicator.activeSelf)
            {
                teleportIndicator.SetActive(true); // Show indicator if it’s not already active
            }
        }
        else
        {
            if (teleportIndicator.activeSelf)
            {
                teleportIndicator.SetActive(false); // Hide indicator if not valid
            }
        }
    }

    private void OnTeleportActionPerformed(InputAction.CallbackContext ctx)
    {
        // Show the indicator when the teleport action is performed
        if (!teleportIndicator.activeSelf)
        {
            teleportIndicator.SetActive(true);
        }
    }

    private void OnTeleportActionCanceled(InputAction.CallbackContext ctx)
    {
        if (teleportIndicator.activeSelf)
        {
            // Teleport to the indicator's position
            Vector3 targetPosition = teleportIndicator.transform.position;

            if (Physics.Raycast(targetPosition + Vector3.up, Vector3.down, out RaycastHit groundHit, 1f, teleportMask))
            {
                targetPosition.y = groundHit.point.y; // Adjust y position to ground level
            }
            else
            {
                Debug.LogWarning("Teleport failed: No valid ground found, teleporting to a default height.");
                targetPosition.y += 1.0f; // Adjust Y position to a default height (modify as necessary)
            }

            player.GetComponent<XROrigin>().MoveCameraToWorldLocation(targetPosition); // Move the XR rig (player) to the target position
            
            // Hide the indicator after teleportation is finished
            teleportIndicator.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        teleportAction.action.performed -= OnTeleportActionPerformed;
        teleportAction.action.canceled -= OnTeleportActionCanceled;
        Destroy(teleportIndicator); // Clean up the teleport indicator
    }
}
