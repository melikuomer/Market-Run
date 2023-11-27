using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.VFX;

public class BuildingSiteScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Material mat;
    public GameObject gameObj;
    public int requiredAmount;
    int currentAmount = 0;
    public TextMeshPro text;
    private OrderSystem orderSystem;
    private MoneySystem ms;
    private VisualEffect visualEffect;
    [SerializeField] private int paySpeed = 100;
    [SerializeField]private bool isStartTile = false;
    private bool canBuild =false;

    private ColorChangerBeta ccb;

    private ProgressBar progressBar;
    //Collider collider;
    private void Start()
    {
        progressBar = FindObjectOfType<ProgressBar>();
        ccb = new ColorChangerBeta();
        ccb.Setup(gameObj.GetComponent<MeshRenderer>(), mat);
        visualEffect = GetComponentInChildren<VisualEffect>();
        orderSystem = FindObjectOfType<OrderSystem>();
        text.text = (requiredAmount - currentAmount).ToString();
        if (isStartTile) SetupBuilding();
        
    }

    
    void SpawnOrder_OnSpawnUpdate(object sender, OrderSystem.OnSpawnUpdateEventArgs e)
    {
        if (GetComponentInChildren<Order>() || UnityEngine.Random.Range(0, 7) <= 2) return;

        GameObject game = new GameObject("NewOrder");
        game.transform.parent = transform;
        game.transform.localScale = new Vector3(1, 2, 1);
        game.transform.localPosition = new Vector3(0, 0, 0);
        OrderSystem.OrderableItems ordItem = e.orderableItems[UnityEngine.Random.Range(0, e.orderableItems.Count)];
        Order od = game.AddComponent<Order>();
        od.orderIndicator = ordItem.indicator;
        od.orderName = ordItem.orderableName;
        orderSystem.AddToList(new OrderSystem.OrderType{location = transform.position, orderedItemName = ordItem.orderableName});
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        ms = other.GetComponentInParent<MoneySystem>();
        if (!canBuild&&ms.RemoveMoney(requiredAmount / paySpeed))
        {
            currentAmount += requiredAmount/ paySpeed;
            text.text = (requiredAmount - currentAmount).ToString();
        }

        if (currentAmount != requiredAmount) return;
        canBuild = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (canBuild)
        {
            SetupBuilding();

        }
    }

    private void SetupBuilding()
    {
        visualEffect.Play();
        ccb.RevertMaterial();
        orderSystem.OnSpawnUpdate += SpawnOrder_OnSpawnUpdate;
        Destroy(GetComponent<Collider>());
        Destroy(text.gameObject);
        progressBar.OnHouseBought();
        Destroy(GetComponent<SpriteRenderer>());
    }
}
