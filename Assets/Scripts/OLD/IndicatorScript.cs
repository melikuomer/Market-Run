using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorScript : MonoBehaviour
{
    public GameObject Indicator;
    
    GameObject Target;
    Renderer rd;
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        rd = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!rd.isVisible)
        {

            if (!Indicator.activeSelf)
            {
                Indicator.SetActive(true);
            }
            int layerMask = 1<<8;
            Vector3 direction = Target.transform.position - transform.position;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hitData;
            
            if(Physics.Raycast(ray, out hitData, Mathf.Infinity, layerMask))
            {
                Debug.Log(message: "hit");
            }



            if (hitData.collider && hitData.collider.name == "Collider") 
            {
                Indicator.transform.position = hitData.point;
            }
        }
        else
        {
            if (Indicator.activeSelf)
            {
                Indicator.SetActive(false);
            }
        }
    }
}
