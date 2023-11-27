using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySpawner : MonoBehaviour
{

    [SerializeField] private GameObject moneyObject;
    [SerializeField] private GameObject moneyBag;
    [SerializeField] private int moneyCount = 5;


    public void SpawnMoney ()
    {
        for (int i = 0; i < moneyCount; i++)
        {

            GameObject gb = Instantiate<GameObject>(moneyObject, transform.position,Random.rotation);
            gb.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-4f, 4f), 7, Random.Range(-4f, 4f)));
        }

    }

    public void SpawnMoney(Vector3 location)
    {
        for (int i = 0; i < moneyCount; i++)
        {

            GameObject gb = Instantiate<GameObject>(moneyObject, location, Random.rotation);
            gb.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-4f, 4f), 7, Random.Range(-4f, 4f)));
        }

    }

    public void SpawnMoneyBag(Vector3 location)
    {
        Instantiate(moneyBag, location, Quaternion.identity);
    }
}
