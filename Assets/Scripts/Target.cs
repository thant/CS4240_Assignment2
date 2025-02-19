using UnityEngine;

public class Target : MonoBehaviour
{
    public float speed = 2.0f; // Movement speed of the target
    public Vector3 direction; // Direction in which the target moves
    private Quaternion originalRotation; // Store the original rotation

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip hitSound;

    private void Start()
    {
        // Set a random movement direction and store the original rotation
        direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        originalRotation = transform.rotation; // Store original rotation

        // Start the coroutine to change direction every 2 seconds
        StartCoroutine(ChangeDirection());
    }

    private void Update()
    {
        // Move the target in the defined direction
        transform.Translate(direction * speed * Time.deltaTime);
        transform.rotation = originalRotation; // Keep the original rotation
    }

    private System.Collections.IEnumerator ChangeDirection()
    {
        while (true)
        {
            // Wait for 2 seconds
            yield return new WaitForSeconds(2f);
            // Change direction randomly
            direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is a rock
        if (collision.gameObject.CompareTag("Rock"))
        {
            PlayHitSound();
            DestroyTarget();
        }
    }

    private void PlayHitSound()
    {
        if (audioSource && hitSound)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    private void DestroyTarget()
    {
        ScoreManager.Instance.AddScore(1); // Add score
        Destroy(gameObject); // Destroy the target
    }
}
