using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.VFX;

public class UpgradeScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameObj;
    public int requiredAmount;
    public TextMeshPro[] text;
    private MoneySystem ms;


    //Collider collider;
    void Start()
    {
        text = GetComponentsInChildren<TextMeshPro>();
        foreach (var t in text)
            t.text = requiredAmount.ToString();
    }

    

    public void Upgrade()
    {
        gameObj.SetActive(true);
        Destroy(gameObject);

    }
    

}
