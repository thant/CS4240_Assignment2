using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton instance

    public int score = 0; // Current score
    public TextMeshProUGUI scoreText; // UI Text to display the score

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Set the singleton instance
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }

    private void Start()
    {
        UpdateScoreUI(); // Update UI at start
    }

    public void AddScore(int value)
    {
        score += value; // Add to score
        UpdateScoreUI(); // Update UI
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score; // Update the score display
        }
    }
}
