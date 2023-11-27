using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    [SerializeField] private GameObject objToServe;
    StackingBackpack backpack;
    Coroutine coroutine;
    // Start is called before the first frame update
   private void OnTriggerEnter(Collider other)
    {

        coroutine = StartCoroutine(Distribute());
        backpack = other.GetComponent<StackingBackpack>();
    }

    private void OnTriggerExit(Collider other)
    {
        StopAllCoroutines();
    }

    IEnumerator Distribute()
    {
        while (true) { 
        yield return new WaitForSeconds(1);
            if (!backpack.IsInventoryFull()) backpack?.AddToBackpack(objToServe);
            else StopAllCoroutines();
        }
    }
}
