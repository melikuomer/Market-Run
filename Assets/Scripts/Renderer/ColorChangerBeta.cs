using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangerBeta 
{
    [SerializeField] private Material ghostMaterial;

    private Material temp;
    private Material[] orgMaterials;
    private Material[] materials;
    public MeshRenderer meshRenderer;

    public void Setup(MeshRenderer renderer, Material material)
    {
        ghostMaterial = material;
        meshRenderer = renderer;
        if(renderer == null) return;
        orgMaterials = new Material[meshRenderer.materials.Length];
        materials = new Material[meshRenderer.materials.Length];
        for (int i = 0; i < meshRenderer.materials.Length; i++)
        {
            orgMaterials[i] = meshRenderer.materials[i];
        }
        ApplyMaterial();
    }


    private void ApplyMaterial()
    {

        for (int i = 0; i < meshRenderer.materials.Length; i++)
        {
            materials[i] = ghostMaterial;
        }
        meshRenderer.materials = materials;
    }

    public void RevertMaterial()
    {
        meshRenderer.materials = orgMaterials;
    }
}
