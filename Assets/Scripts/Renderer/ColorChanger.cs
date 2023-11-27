using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Material ghostMaterial;

    private Material temp;
    private Material[] orgMaterials;
    private MeshRenderer meshRenderer;

    private void OnValidate()
    {
        
        meshRenderer = GetComponent<MeshRenderer>();
        orgMaterials = new Material[meshRenderer.materials.Length];
        for (int i = 0; i < meshRenderer.materials.Length; i++) { 
            orgMaterials[i] = new Material(meshRenderer.materials[i]);
        }
    }
    public void ApplyGhostMaterial()
    {

        for (int i = 0; i < meshRenderer.materials.Length; i++)
        {
            meshRenderer.materials[i].CopyPropertiesFromMaterial(ghostMaterial);
        }
    }

    public void RevertGhostMaterial()
    {
        for (int i = 0; i < meshRenderer.materials.Length; i++)
        {
            meshRenderer.materials[i].CopyPropertiesFromMaterial(orgMaterials[i]);
        }
    }
}
