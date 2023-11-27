using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Popup : MonoBehaviour
{
   
    public static Popup Create(Vector3 position, int value)
    {
        GameObject popUpObject = Instantiate(GameAssets.i.popupText, position, Quaternion.identity);

        Popup popup = popUpObject.GetComponent<Popup>();
        popup.Setup(value);
        
        return popup;
    }

    private TextMeshPro textMeshPro;
    private float disappearTimer;
    private Color textColor;

    private void Awake()
    {
        textMeshPro = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int value)
    {
        textMeshPro.SetText(value.ToString());
        textColor = textMeshPro.color;
        disappearTimer = 1f;
    }

    private void Update()
    {
        float moveYSpeed = 20f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        if ((disappearTimer -= Time.deltaTime) < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMeshPro.color = textColor;
            if(textColor.a<0) Destroy(gameObject);
        }
    }
}
