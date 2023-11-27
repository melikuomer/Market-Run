using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform objTransform;
    [SerializeField] private Vector3 cameraOffset;

    // Update is called once per frame
    private void Update()
    {
        Vector3 position = objTransform.position;
        transform.position = new Vector3(position.x-cameraOffset.x, position.y-cameraOffset.y, position.z-cameraOffset.z);
    }

    public void SetCameraOffset(Vector3 offset)
    {
        cameraOffset = offset;
    }

    public Vector3 GetCameraOffset()
    {
        return cameraOffset;
    }
}
