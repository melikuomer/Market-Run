using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Order : MonoBehaviour
{
    BoxCollider box;
    public GameObject orderIndicator;
    public string orderName;
    int orderAmount = 2;
    MoneySpawner moneySpawner;
    // Start is called before the first frame update
    void Awake()
    {
        
        box = gameObject.AddComponent<BoxCollider>();
        box.size = new Vector3(0.5f, 2, 1f);
        box.center = new Vector3(0, 0.9f, -0.2f);
        box.isTrigger = true;
        
    }

    private void Start()
    {
        moneySpawner = GetComponentInParent<MoneySpawner>();
        Instantiate<GameObject>(orderIndicator, transform.position + Vector3.up * 10, Quaternion.identity).transform.parent = transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        StackingBackpack sb;
        if (!other.CompareTag("Player") && !other.CompareTag("Worker")) return;
        sb = other.GetComponent<StackingBackpack>();

        StartCoroutine( Wait(0.5f,sb));
    }

    IEnumerator Wait(float seconds,StackingBackpack sb) {
        while (orderAmount > 0)
        {
            yield return new WaitForSeconds(seconds);
            if (sb.RemoveFromBackpack(orderName)) orderAmount--;
            else break;

        }
        if (orderAmount == 0)
        {
            if (Random.Range(0, 9) < 2) moneySpawner.SpawnMoneyBag(transform.position - Vector3.forward * 9f);
            moneySpawner.SpawnMoney(transform.position-Vector3.forward*9f);
            var gb = Instantiate(GameAssets.i.reactions[Random.Range(0,GameAssets.i.reactions.Length)], transform.position+Vector3.up*8f,Quaternion.identity);
            Utils.Utils.DelayAction(3000,()=>Destroy(gb));
            Destroy(gameObject);
        }
        yield break;
    }
}
