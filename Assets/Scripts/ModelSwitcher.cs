using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ModelSwitcher : MonoBehaviour
{
    public GameObject[] models; // Array of models to switch between
    public InputActionProperty buttonB; // B button input action
    private int currentModelIndex = 0; // Keep track of the currently active model

    public GameObject infoPanel; // UI Panel for displaying model info
    public TextMeshPro titleText; // UI Text component for model info
    public TextMeshPro infoText; // UI Text component for model info
    public Renderer infoImageRenderer;
    public string[] modelDescriptions; // Descriptions for each model

    public string[] modelTitles;
    public Texture[] modelImages; // Images for each model

    void Start()
    {
        // Disable all models initially
        foreach (GameObject model in models)
        {
            model.SetActive(false);
        }
        
        // Enable the first model and update info panel
        if (models.Length > 0)
        {
            models[currentModelIndex].SetActive(true);
            UpdateInfoPanel();
        }
    }

    void Update()
    {
        // Check if the B button was pressed this frame
        if (buttonB.action.WasPressedThisFrame())
        {
            // Disable the current model
            models[currentModelIndex].SetActive(false);

            // Increment the index to switch to the next model
            currentModelIndex = (currentModelIndex + 1) % models.Length;

            // Enable the new model and update info panel
            models[currentModelIndex].SetActive(true);
            UpdateInfoPanel();
        }
    }

    void UpdateInfoPanel()
    {
        if (infoPanel != null && infoText != null && infoImageRenderer != null)
        {
            infoText.text = modelDescriptions[currentModelIndex];
            titleText.text = modelTitles[currentModelIndex];
            infoImageRenderer.material.mainTexture = modelImages[currentModelIndex];
        }
    }
}
