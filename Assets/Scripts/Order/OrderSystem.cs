using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSystem : MonoBehaviour
{
    public event EventHandler<OnSpawnUpdateEventArgs> OnSpawnUpdate;
    readonly int maxWorkerCount = 10;
    private int workerCost = 100;
    private int orderUpgradeCost = 100;
    private float orderCooldown = 3f;
    [SerializeField]private List<WorkerScript> workerScripts;
    [SerializeField] private Transform workerBase;
    [SerializeField] private GameObject worker;

    [SerializeField] private GameObject[] objectStations;
    [SerializeField] private GameObject[] objectIndicators;

    private int orderableCount = 2;
    private int moreOrderableItemCost = 100;
    public struct OrderType
    {
        public Vector3 location;
        public string orderedItemName;
    }   
    private List <OrderType> orderList;

    public struct OrderableItems
    {
        public GameObject indicator;
        public string orderableName;
    }
    private List<OrderableItems> orderableItems;
    public class OnSpawnUpdateEventArgs : EventArgs
    {
        public List<OrderableItems> orderableItems;
    }
    public List<GameObject> orderables;
    private void Start()
    {
        orderableItems = new List<OrderableItems>();
        orderableItems.Add(new OrderableItems { indicator = objectIndicators[0], orderableName = orderables[0].name });
        orderableItems.Add(new OrderableItems { indicator = objectIndicators[1], orderableName = orderables[1].name });
        orderList = new List<OrderType>();
        StartCoroutine(Spawn());
    }
    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(orderCooldown);
            OnSpawnUpdate?.Invoke(this, new OnSpawnUpdateEventArgs { orderableItems = orderableItems });
            OnWorkerAvailable();
        }
    }

    public void AddToList(OrderType order)
    {
        orderList.Add(order);
    }

 
    private void OnWorkerAvailable ()
    {
        foreach (var workerScript in workerScripts)
        {
            if (orderList.Count <= 0 || !workerScript.GetIsAvailable()) continue;
            OrderType ot = orderList[UnityEngine.Random.Range(0, orderList.Count)];
            Vector3 tempPos = Vector3.zero;
            switch (ot.orderedItemName)
            {
                case "MilkCart":
                    tempPos = objectStations[0].transform.position;
                    break;
                case "FireworkCart":
                    tempPos = objectStations[1].transform.position;
                    break;
                case "ChipsCart":
                    tempPos = objectStations[2].transform.position;
                    break;
                case "MeatCart":
                    tempPos = objectStations[3].transform.position;
                    break;
                default:
                    break;
            }
            workerScript.OnOrderArrival(ot,tempPos);
            orderList.Remove(ot);
        }
    }

    public int TryWorkerUpgrade(int index)
    {
        if (workerScripts.Count > index)
        {
            return workerScripts[index].LevelUp();
        }
        else return -1;
    }

    public int TryGetWorkerLevelUpCost (int index)
    {
        if (workerScripts.Count > index)
        {
            return workerScripts[index].GetWorkerLevelUpCost();
        }
        else return -1;
    }

    public int UpgradeOrderCooldownReduction()
    {

        orderCooldown = orderCooldown * 0.9f;
        orderUpgradeCost = (int)(orderUpgradeCost * 1.5f);
        return orderUpgradeCost;
    }

    public int GetOrderCooldownUpgradeCost()
    {
        return orderUpgradeCost;
    }

    public int GetNewWorker()
    {
        if (workerScripts.Count > maxWorkerCount) return -1;
        workerScripts.Add(Instantiate(worker, workerBase.position, workerBase.rotation).GetComponent<WorkerScript>());
        return workerCost;

    }
    public int GetNewWorkerCost()
    {

        return (maxWorkerCount == workerScripts.Count) ? -1 : workerCost;
    }


    public int MoreOrderTypes()
    {
        if (orderableCount == orderables.Count) return -1;
        orderableItems.Add(new OrderableItems { indicator = objectIndicators[orderableCount], orderableName = orderables[orderableCount].name });
        //set order station active
        objectStations[orderableCount].SetActive(true);
        //let houses order from new station
        
        orderableCount++;
        return moreOrderableItemCost;
    }

    public int GetMoreOrderTypesCost()
    {
        return moreOrderableItemCost;
    }
}
