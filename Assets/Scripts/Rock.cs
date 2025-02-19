using UnityEngine;

public class Rock : MonoBehaviour
{
    private Rigidbody rb;
    public float destroyDelay = 2f; // Optional: Delay before destroying the rock
    private bool isThrown = false; // Flag to check if the rock has been thrown

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Check if the rock has been thrown
        if (isThrown)
        {
            // Check if the Rigidbody's velocity is near zero
            if (rb != null && rb.linearVelocity.magnitude < 0.1f)
            {
                // Optionally, you can add a delay before destruction
                Destroy(gameObject, destroyDelay); // Destroy the rock after a brief delay
            }
        }
    }

    // Call this method to mark the rock as thrown
    public void MarkAsThrown()
    {
        isThrown = true;
    }
}
