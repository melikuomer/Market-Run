using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMovement : MonoBehaviour
{
    [SerializeField] [Range (20,300)] float speed = 100f;
    private DynamicJoystick joystick;

    public Transform motorCycle;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<DynamicJoystick>();
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += new Vector3(joystick.Horizontal , transform.position.y, joystick.Vertical);
        motorCycle.transform.LookAt(motorCycle.transform.position + rb.velocity);
        
    }
}
