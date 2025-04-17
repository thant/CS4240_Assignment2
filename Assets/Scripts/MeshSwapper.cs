using UnityEngine;
using UnityEngine.InputSystem;

public class MeshSwapper : MonoBehaviour
{
    public InputActionProperty swapButton;               // Bind the button press input action here
    public GameObject sourceObject;                      // Drag the mesh+material source object here
    public Vector3 rotationOffsetForSwappedMesh;         // Offset in degrees (Euler)

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private Mesh originalMesh;
    private Material[] originalMaterials;
    private Quaternion originalRotation;

    private bool isSwapped = false;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        originalMesh = meshFilter.sharedMesh;
        originalMaterials = meshRenderer.sharedMaterials;

        originalRotation = transform.rotation; // Save the original rotation
    }

    void Update()
    {
        // Check if the swap button is pressed
        if (swapButton.action.triggered)
        {
            SwapMeshAndMaterial();
        }
    }

    // This method swaps the mesh and material
    void SwapMeshAndMaterial()
    {
        if (!isSwapped)
        {
            var sourceFilter = sourceObject.GetComponent<MeshFilter>();
            var sourceRenderer = sourceObject.GetComponent<MeshRenderer>();

            meshFilter.sharedMesh = sourceFilter.sharedMesh;
            meshRenderer.sharedMaterials = sourceRenderer.sharedMaterials;

            transform.rotation = Quaternion.Euler(originalRotation.eulerAngles + rotationOffsetForSwappedMesh);
        }
        else
        {
            meshFilter.sharedMesh = originalMesh;
            meshRenderer.sharedMaterials = originalMaterials;
            transform.rotation = originalRotation;
        }

        isSwapped = !isSwapped;
    }
}
