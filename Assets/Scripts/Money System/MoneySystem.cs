using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoneySystem : MonoBehaviour
{
    [SerializeField]private int money=100;
    private int itemPrice = 10;
    private int upgradeCost = 100;

    public event EventHandler<OnBalanceChangedEventArgs> OnBalanceChanged;

    public class OnBalanceChangedEventArgs : EventArgs
    {
        public int money;
    }
    private void Start()
    {
        OnBalanceChanged?.Invoke(this, new OnBalanceChangedEventArgs { money = money });
    }
    public void AddMoney(int value)
    {
        //money += value * itemPrice;
        money += value;
        OnBalanceChanged?.Invoke(this, new OnBalanceChangedEventArgs { money = money });
    }

    public bool RemoveMoney (int value)
    {
        if (value <= money)
        {
            money -= value;
            OnBalanceChanged?.Invoke(this, new OnBalanceChangedEventArgs { money = money });
            return true;
        }
        else return false;
    }

    public int UpgradeOrderPrice()
    {
        itemPrice = (int)(1.1 * itemPrice);
        upgradeCost = (int)(1.2 * upgradeCost);

        return upgradeCost;
    }

    public int GetUpgradeCost()
    {
        return upgradeCost;
    }
}
