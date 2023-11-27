using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruction : MonoBehaviour
{
    // Start is called before the first frame update
    private float orderTimeLimit = 30f;
    CharacterMovement cm;
    void Start()
    {
        StartCoroutine(SelfDestroy());
    }

    // Update is called once per frame
    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(orderTimeLimit);
        Destroy(gameObject);
    }
}
