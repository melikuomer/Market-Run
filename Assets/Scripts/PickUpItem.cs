using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private MoneySystem ms;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Money"))
        {
            Destroy(other.gameObject);
            ms.AddMoney(10);
            Popup.Create(transform.position, 10);
        } else if (other.CompareTag("MoneyBag"))
        {
            Destroy(other.gameObject);
            ms.AddMoney(1000);
            Popup.Create(transform.position, 1000);
        }
    }
}
