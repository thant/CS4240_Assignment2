using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.OpenXR.Input;

public class Grab : MonoBehaviour
{
    public InputActionReference grabShootAction; // Action for grabbing and shooting
    public InputActionProperty haptic; // Haptic feedback properties
    public float amplitude = 1.0f;
    public float duration = 0.1f;
    public float frequency = 50.0f;

    private GameObject grabbedObject; // Currently grabbed object
    public float grabRadius; // Radius for grabbing objects
    public LayerMask grabMask; // Layer mask to define which objects can be grabbed

    private bool isGrabbing = false; // Check if grabbing is active

    public GameObject rockPrefab; // Reference to the rock prefab

    public AudioSource audioSource;
    public AudioClip grabSound;
    public AudioClip shootSound;

    private Spawner spawner; // Reference to the Spawner script

    void Start()
    {
        if (grabShootAction == null || haptic == null)
        {
            return;
        }

        grabShootAction.action.Enable();
        grabShootAction.action.performed += OnGrabShootPerformed;

        // Use the updated method to find the Spawner component
        spawner = FindFirstObjectByType<Spawner>(); // Use FindFirstObjectByType for the Spawner
    }

    void OnGrabShootPerformed(InputAction.CallbackContext ctx)
    {
        if (!isGrabbing)
        {
            GrabObject();
        }
        else
        {
            Shoot();
        }
    }

    void GrabObject()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, grabRadius, transform.forward, 0f, grabMask);

        if (hits.Length > 0)
        {
            int closestHit = 0;
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].distance < hits[closestHit].distance)
                {
                    closestHit = i;
                }
            }

            grabbedObject = hits[closestHit].transform.gameObject;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            grabbedObject.transform.position = transform.position;
            grabbedObject.transform.parent = transform; // Parent to the controller

            // Set the tag to "Rock"
            grabbedObject.tag = "Rock";
            if (audioSource && grabSound)
                audioSource.PlayOneShot(grabSound);

            // Haptic feedback for grabbing
            var control = grabShootAction.action.activeControl;
            if (control != null)
            {
                OpenXRInput.SendHapticImpulse(haptic.action, amplitude, frequency, duration, control.device);
            }

            isGrabbing = true; // Set grabbing state
        }
    }

    void Shoot()
    {
        if (grabbedObject != null)
        {
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                grabbedObject.transform.parent = null; // Unparent the rock
                rb.isKinematic = false; // Make it non-kinematic to apply physics
                rb.AddForce(transform.forward * 10f, ForceMode.Impulse); // Shoot the rock with a specified force
                if (audioSource && shootSound)
                    audioSource.PlayOneShot(shootSound);
                ReleaseObject(); // Release the object after shooting
            }
        }
    }

    void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            grabbedObject.transform.parent = null; // Unparent the object
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false; // Make it non-kinematic again

            // Mark the rock as thrown
            Rock rockScript = grabbedObject.GetComponent<Rock>();
            if (rockScript != null)
            {
                rockScript.MarkAsThrown(); // Indicate that the rock has been thrown
            }

            grabbedObject = null; // Reset the grabbed object
            isGrabbing = false; // Reset grabbing state
            
            // Call SpawnPrefab method on the spawner
            spawner.SpawnPrefab(); // Call this method directly if it’s public
        }
    }
}
