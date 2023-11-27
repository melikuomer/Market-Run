using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDelay : MonoBehaviour
{
    // Start is called before the first frame update
    BoxCollider box;
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        box = GetComponent<BoxCollider>();
        box.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.y<0&&gameObject.transform.position.y < 0.6f)
        {
            box.enabled = true;
            Destroy(this);
        }
    }
}
