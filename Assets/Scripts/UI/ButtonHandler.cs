using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private MoneySystem ms;
    private int money;
   
    [SerializeField] private OrderSystem os;
    [SerializeField] private TextMeshProUGUI[] costs;
    private void Awake()
    {
        // tmp = GetComponents<TextMeshPro>()[0];
        ms.OnBalanceChanged += ButtonHandler_OnBalanceChange;
    }
    public void ButtonHandler_OnBalanceChange(object sender, MoneySystem.OnBalanceChangedEventArgs e)
    {
        money = e.money;
    }



    public void Worker_OnButtonClick(int index)
    {
        int requiredAmount;
        if ((requiredAmount = os.TryGetWorkerLevelUpCost(index)) == -1) return;
        if ( requiredAmount> money) return;
            
            
        //tmp.text = os.TryWorkerUpgrade(index).ToString();
        os.TryWorkerUpgrade(index);
        ms.RemoveMoney(requiredAmount); 
            
        
    }

    public void ShopIncomeIncrease_OnButtonClick()
    {
        int requiredAmount = ms.GetUpgradeCost();
        if (requiredAmount > money) return;
        
        //tmp.text = ms.UpgradeOrderPrice().ToString();
        costs[0].text = ms.UpgradeOrderPrice().ToString();
        ms.RemoveMoney(requiredAmount);
        
        
    }

    public void ShopOrderIncrease_OnButtonClick()
    {
        int requiredAmount = os.GetOrderCooldownUpgradeCost();
        if (requiredAmount > money) return;
        costs[1].text = os.UpgradeOrderCooldownReduction().ToString();
        ms.RemoveMoney(requiredAmount);
        //tmp.text = os.UpgradeOrderCooldownReduction().ToString(); ;
       
    }

    public void ShopMoreWorker_OnButtonClick()
    {


        int requiredAmount;
        if ((requiredAmount = os.GetNewWorkerCost()) == -1)return;
        if (requiredAmount > money) return;
        costs[2].text = os.GetNewWorker().ToString();
        ms.RemoveMoney(requiredAmount);

    }

    public void ShopMoreItems_OnButtonClick()
    {
        int requiredAmount = os.GetMoreOrderTypesCost();
        if (requiredAmount > money) return;
        if (os.MoreOrderTypes() == -1) return;

        ms.RemoveMoney(requiredAmount);
        costs[3].text = os.GetMoreOrderTypesCost().ToString();
        

    }

}
