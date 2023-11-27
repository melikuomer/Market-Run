using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestourantScript : MonoBehaviour
{
    public GameObject spawner;
    public GameObject[] food;
    public GameObject[] client;
    public float interval = 2f;
    [SerializeField] [Range (0,1)] float chance = 0.6f;
    private IEnumerator coroutine;
    private bool isSpawnable = true;
    private LevelCreatorScript lcs;
    // Start is called before the first frame update
    void Start()
    {
        //lcs = spawner.GetComponent<LevelCreatorScript>();
        coroutine = WaitAndNewOrder(interval);
        StartCoroutine(coroutine);
        lcs = FindObjectOfType<LevelCreatorScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator WaitAndNewOrder (float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            if (!GetComponentInChildren<SpriteRenderer>()) isSpawnable = true;
            if(isSpawnable)
            {
                if (Random.Range(0f, 1f) < chance)
                {
                    int tmp = Random.Range(0, food.Length);
                    Instantiate<GameObject>(food[0], transform).AddComponent<SelfDestruction>();
                    lcs.SpawnClient(client[0]);
                    isSpawnable = false;
                }
            }
        }
    }


}
