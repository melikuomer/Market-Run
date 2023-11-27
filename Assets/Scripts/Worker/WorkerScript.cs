using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorkerScript : MonoBehaviour
{

    [SerializeField] Vector3 targetPos;


    [SerializeField]private int requiredCost=100;
    [SerializeField] private float speed = 10f;
    private Vector3 stationPos;
    private Vector3 idlePos;
    private bool isArrived = true;
    private bool isAvailable = true;
    private bool didGetItems = true;
    private bool doOnce = true;
    private BoxCollider boxCollider;
    
    private Animator animator;
    //private OrderSystem.OrderType orderType;
    private StackingBackpack sb;
    [SerializeField]private Transform drone;

    
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int HasOrder = Animator.StringToHash("HasOrder");

    private void Start()
    {
        
        animator = GetComponentInChildren<Animator>();
        sb = GetComponent<StackingBackpack>();
        boxCollider = GetComponent<BoxCollider>();
        idlePos = transform.position;
        
    }

    private void Update()
    {
        AnimationController(transform.position);
        if (!didGetItems)
        {

            boxCollider.enabled = false;
            MoveToOrder(stationPos);
            if (transform.position.x == stationPos.x && transform.position.z == stationPos.z)
            {
                boxCollider.enabled = true;

            }
            if (sb.IsInventoryFull())
            {
                boxCollider.enabled = false;
                didGetItems = true;
                isArrived = false;

            }
        }
        else if (!isArrived) MoveToOrder(targetPos);
        else 
        {
            boxCollider.enabled = true;
            if (doOnce)
            {

                doOnce = false;
                StartCoroutine(sb.InventoryCountdown(3));
            }
            if ((sb.GetCurrentInventory() == 0)) { 
                if (transform.position != idlePos)
                {

                    MoveToOrder(idlePos);
                }
                else
                {

                    isAvailable = true;
                }
            }
        }
    }
    private void MoveToOrder(Vector3 target)
    {
        Vector3 direction;
        var position = transform.position;
        if ((int)position.x%21!=0&&target.x != position.x)
        {
            direction = new Vector3(target.x, position.y, position.z);
            drone.LookAt(direction);
            transform.position = Vector3.MoveTowards(position, direction, speed);
        }
        else if (position.z > target.z|| position.z < target.z)
        {
            direction = new Vector3(position.x, position.y, target.z);
            drone.LookAt(direction);
            transform.position = Vector3.MoveTowards(position, direction, speed);
        }
        else if (position.x >target.x|| position.x < target.x)
        {
            direction = new Vector3(target.x, position.y, position.z);
            drone.LookAt(direction);
            transform.position = Vector3.MoveTowards(position,direction, speed);
        }
        else isArrived = !isArrived;
    }


    public void OnOrderArrival(OrderSystem.OrderType order,Vector3 position)
    {
        isAvailable = false;
        targetPos = order.location-Vector3.forward*8f;
        //orderType.orderedItem = order.orderedItem;
        isArrived = false;
        didGetItems = false;
        stationPos = position-Vector3.forward*5f;
        doOnce = true;
    }


    public bool GetIsAvailable()
    {
        return isAvailable;
    }

    public int LevelUp()
    {
        speed *= 1.1f;
        requiredCost = (int)(1.5*requiredCost);
        return requiredCost;
    }

    public int GetWorkerLevelUpCost()
    {
        return requiredCost;
    }


    private  Vector3 positionLastFrame = Vector3.negativeInfinity;
    private void AnimationController(Vector3 position)
    {
        animator?.SetBool(IsWalking, position != positionLastFrame);
        positionLastFrame = position;
        if (isArrived&&sb.GetCurrentInventory()==0) animator?.SetBool(HasOrder, false);
        else if (didGetItems)animator?.SetBool(HasOrder, true);

    }
}
