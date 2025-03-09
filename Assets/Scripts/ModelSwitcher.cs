using UnityEngine;

public class ModelSwitcher : MonoBehaviour
{
    public GameObject[] models; // Array of models to switch between
    private int currentModelIndex = 0; // Keep track of the currently active model

    void Start()
    {
        // Disable all models initially
        foreach (GameObject model in models)
        {
            model.SetActive(false);
        }
        
        // Enable the first model
        if (models.Length > 0)
        {
            models[currentModelIndex].SetActive(true);
        }
    }

    void Update()
    {
        // Check for right-click input (mouse button 1 is the right mouse button)
        if (Input.GetMouseButtonDown(1)) // Right-click
        {
            // Disable the current model
            models[currentModelIndex].SetActive(false);

            // Increment the index to switch to the next model
            currentModelIndex = (currentModelIndex + 1) % models.Length;

            // Enable the new model
            models[currentModelIndex].SetActive(true);
        }
    }
}
