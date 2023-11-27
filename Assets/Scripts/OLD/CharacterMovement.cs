using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Text coinValue, sushiValue, pizzaValue, hamburgerValue, reputationValue;
    public Image energyBar,reputationBar;
    public Rigidbody rb;
    public GameObject [] foods;
    [SerializeField] [Range(1, 20)] float speed = 2f;

    
    private StackingBackpack backpack;
    private float energy = 800f;
    private float reputation = 100f;
    private int coin, sushi, pizza, hamburger;
    public Transform playerTransform;
    void Start()
    {
        coin = 0;
        sushi = 0;
        pizza = 0;
        hamburger = 0;
        backpack = FindObjectOfType<StackingBackpack>();
            
    }

    private void OnTriggerEnter(Collider other)
    {
        SpriteRenderer st = other.GetComponentInChildren<SpriteRenderer>();
        if (other.gameObject.CompareTag("Restaurant"))
        {
            switch (st.sprite.name)
            {
                case "sus":
                    print("sus");
                    sushi++;
                    break;
                case "hamburger":
                    print("hamburger");
                    hamburger++;
                    backpack.AddToBackpack(foods[0]);
                    break;
                case "pizza":
                    print("pizza");
                    pizza++;
                    break;
                default:
                    print("error");
                    break;
            }
            Destroy(other.gameObject);
        } else if (other.gameObject.CompareTag("Client"))
        {
            switch (st.sprite.name)
            {
                case "sus":
                    print("sus");
                    if (sushi > 0)
                    {
                        sushi--;
                        coin += 30;
                        Destroy(other.gameObject);
                    }
                    break;
                case "hamburger":
                    print("hamburger");
                    if (hamburger > 0)
                    {
                        coin += 30;
                        //backpack.RemoveFromBackpack(foods[0]);
                        hamburger--;
                        Destroy(other.gameObject);
                    }
                    break;
                case "pizza":
                    print("pizza");
                    if (pizza > 0)
                    {
                        coin += 30;
                        pizza--;
                        Destroy(other.gameObject);
                    }
                    break;
                default:
                    print("error");
                    break;
            }
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        energy--;
        coinValue.text = coin.ToString();
        sushiValue.text = sushi.ToString();
        pizzaValue.text = pizza.ToString();
        hamburgerValue.text = hamburger.ToString();
        if(energy>1)
        energyBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, energy);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rb.velocity = Vector3.left * speed;
            playerTransform.localRotation = Quaternion.Euler(Vector3.Lerp(playerTransform.rotation.eulerAngles, new Vector3(0, 270, 0), 1f));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rb.velocity = Vector3.right * speed;
            playerTransform.localRotation = Quaternion.Euler(Vector3.Lerp(playerTransform.rotation.eulerAngles, new Vector3(0, 90, 0), 1f));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            rb.velocity = Vector3.forward * speed;
            playerTransform.localRotation = Quaternion.Euler(Vector3.Lerp(playerTransform.rotation.eulerAngles, new Vector3(0, 0, 0), 1f));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            rb.velocity = Vector3.back * speed;
            playerTransform.localRotation = Quaternion.Euler(Vector3.Lerp(playerTransform.rotation.eulerAngles, new Vector3(0, 180, 0), 1f));
        }
    }

    public void onOrderTimeout ()
    {
        reputation -=3;
        reputationBar.fillAmount = reputation / 100;
        reputationValue.text = reputation.ToString();
    }
}
