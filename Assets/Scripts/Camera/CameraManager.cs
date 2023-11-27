using Unity.VisualScripting;
using UnityEngine;
using System;

namespace Camera
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private float offsetIncrement;
        private FollowCamera fc;
        private ProgressBar pb;
        private void Start()
        {
            pb = GameObject.FindObjectOfType<ProgressBar>();
            fc = gameObject.GetComponent<FollowCamera>();
            pb.OnProgressBarFilled += Camera_OnProgressBarFull;
        }

        private void Camera_OnProgressBarFull(object sender, EventArgs e)
        {
            fc.SetCameraOffset(fc.GetCameraOffset()+ new Vector3(0,-1,1)*offsetIncrement);
        }
    }
}