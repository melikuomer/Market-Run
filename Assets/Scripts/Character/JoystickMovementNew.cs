using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JoystickMovementNew : MonoBehaviour
{
    [SerializeField] [Range (1,300)] private float speed = 20f;
    [SerializeField] [Range(0, 5)] private float speedStep = .2f;
    [SerializeField] [Range(1, 300)] private float maxSpeed = 30f;
    float defaultSpeed;
    private DynamicJoystick joystick;
    private float vertical, horizontal;
    private bool isHolding = false;

    public Transform motorCycle;
    public Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        defaultSpeed = speed;
        joystick = FindObjectOfType<DynamicJoystick>();
        joystick.OnPointerUpEvent += JoystickMovement_OnPointerUp;
        joystick.OnPointerDownEvent += JoystickMovement_OnPointerDown;
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Math.Abs(joystick.Horizontal) > 0 || Math.Abs(joystick.Vertical) > 0) 
            if(speed<maxSpeed)
                speed+=speedStep;

        if (isHolding) {         
        vertical = joystick.Vertical;
        horizontal = joystick.Horizontal;        
        }
        rb.velocity = new Vector3(joystick.Horizontal * speed, rb.velocity.y, joystick.Vertical * speed);
        motorCycle.transform.LookAt(motorCycle.transform.position + rb.velocity);
    }

    private void JoystickMovement_OnPointerUp(object sender,EventArgs e)
    {
        isHolding = false;
        rb.velocity = Vector3.zero;


    }

    private void JoystickMovement_OnPointerDown(object sender, EventArgs e)
    {
        isHolding = true;
        speed = defaultSpeed;
        
    }

    IEnumerator SlowDown()
    {
        while (speed > 1)
        {
            yield return new WaitForEndOfFrame();
            speed -= speedStep;
            rb.velocity = new Vector3(horizontal * speed, rb.velocity.y, vertical * speed);
        }
    }
}
