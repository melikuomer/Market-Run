using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;

    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private UpgradeScript[] upgradeScripts;


    public int houseCount = 5;
    public float fillSpeed = .5f;
    private float targetProgress = 0;
    private int currentHouseCount = 0;
    private int i = 0;



    public event EventHandler OnProgressBarFilled; 

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = 0;
    }

    
    // Start is called before the first frame update
  
    // Update is called once per frame
    private void Update()
    {
        
        if ((1-slider.value)<.05f)
        {
            OnProgressBarFilled(this,EventArgs.Empty);
            targetProgress = 0;
            currentHouseCount = 0;
            textMeshPro.text = (currentHouseCount + "/" + houseCount);
        }

        if (Math.Abs(slider.value - targetProgress) < .05f)
        {
            slider.value = targetProgress;
            return;
        }
        if (slider.value < targetProgress)
        {
            slider.value += fillSpeed * Time.deltaTime;
            
        }else if (slider.value > targetProgress)
        {
            slider.value -= fillSpeed * Time.deltaTime;
        }

        

    }

    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value+ newProgress;
        textMeshPro.text = currentHouseCount + "/" + houseCount;
        if (upgradeScripts.Length !=i&&Math.Abs(targetProgress - 1f) < .1f)
        {
            upgradeScripts[i].Upgrade();
            i++;
            houseCount = 8;
        }
    }

    public void OnHouseBought()
    {
        currentHouseCount++;
        IncrementProgress(1f / houseCount);
    }

}
