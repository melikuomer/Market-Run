using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ColorChanger))]
public class ColorEditor : Editor
{

    public override void OnInspectorGUI()
    {


        base.OnInspectorGUI();
        ColorChanger colorChanger = (ColorChanger)target;

        if (GUILayout.Button("Apply Ghost Material"))
        {
            colorChanger.ApplyGhostMaterial();
        }

        if (GUILayout.Button("Revert Ghost Material"))
        {
            colorChanger.RevertGhostMaterial();
        }
    }


}
