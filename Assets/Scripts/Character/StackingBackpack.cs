using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackingBackpack : MonoBehaviour
{
    [SerializeField] Transform backpack;
    public int maxInventory=20;
    private int currInventory = 0;
    public float itemSpacing = 10f;
    private List<GameObject> inventory;
    [SerializeField] Vector3 offset = Vector3.zero;
    [SerializeField] int inventoryCooldown = 20;
    [SerializeField] bool isTemporary = false;
    // Start is called before the first frame update

    void Start()
    {
        backpack = backpack ? backpack : transform;
        inventory = new List<GameObject>();
        if (isTemporary) StartCoroutine(InventoryCountdownLoop(inventoryCooldown));
    }

    public IEnumerator InventoryCountdownLoop(int cooldown)
    {
        while (true) { 
        yield return new WaitForSeconds(cooldown);
            ResetInventory();
        }
    }

    public IEnumerator InventoryCountdown(int cooldown)
    {
            yield return new WaitForSeconds(cooldown);
            ResetInventory();
        
    }

    public void ResetInventory()
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            Destroy(inventory[i]);
        }
        inventory.Clear();
        currInventory = 0;
    }
    public void AddToBackpack(GameObject item)
    {
        if (maxInventory>currInventory) { Vector3 tmp = new Vector3(backpack.transform.position.x, backpack.transform.position.y + inventory.Count * itemSpacing, backpack.transform.position.z)+offset;
            GameObject gb = Instantiate<GameObject>(item, tmp, Quaternion.identity);
            gb.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            StartCoroutine(ScaleAnimation(gb, new Vector3(0.6f, 0.6f, 0.6f)));
            gb.transform.parent = backpack;
            inventory.Add(gb);
            currInventory++;
        }
    }
    public bool RemoveFromBackpack(string item)
    {
        bool isRemoved = false;
        int max = inventory.Count;
        for (int i = 0; i < max; i++)
        {
            if (isRemoved)
            {
                inventory[i-1].transform.position = inventory[i-1].transform.position + Vector3.down * itemSpacing;
                
            } else
            {
                if (inventory[i].name.Contains(item))
                {
                    Destroy(inventory[i]);
                    inventory.RemoveAt(i);
                    isRemoved = true;
                    currInventory--;
                    
                }
                
            }
        }
        return isRemoved;
    }

    public int GetCurrentInventory()
    {
        return currInventory;
    }

    public bool IsInventoryFull ()
    {
        return currInventory == maxInventory;
    }
    public IEnumerator ScaleAnimation(GameObject @object,Vector3 scale)
    {
        while (@object!=null&&scale != @object.transform.localScale) { 
        @object.transform.localScale = Vector3.Lerp(@object.transform.localScale, scale, Time.deltaTime*2);
            yield return 1;

        }
        //yield return null;
    }
}
