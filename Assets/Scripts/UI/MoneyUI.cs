using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private MoneySystem ms;
    [SerializeField] private TextMeshProUGUI TextMeshPro;
    
    void Awake()
    {
        TextMeshPro = GetComponent<TextMeshProUGUI>();
        ms.OnBalanceChanged += UI_OnBalanceChanged;
    }

    private void UI_OnBalanceChanged(object sender, MoneySystem.OnBalanceChangedEventArgs e)
    {
        TextMeshPro.text = e.money.ToString();
        
    }


}
