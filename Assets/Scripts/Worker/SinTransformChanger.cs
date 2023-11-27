using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinTransformChanger : MonoBehaviour
{

    float index = 1;
    [SerializeField] private Vector3 wobbleDensity = new Vector3(0, 0.001f, 0);
    // Update is called once per frame
    void Update()
    {
        MovementDeformer();
    }

    void MovementDeformer()
    {
        index += Time.deltaTime;
        float y = wobbleDensity.y*  Mathf.Sin(1f * index);
        float x = wobbleDensity.x* Mathf.Sin(3f * index);
        float z = wobbleDensity.z * Mathf.Sin(5f * index);
        transform.position += new Vector3(x, y, z);
    }
}
