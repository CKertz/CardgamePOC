using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class CustomerController : MonoBehaviour
{
    public float speed = 2.0f;
    public float maxDistance = 5.0f; // the maximum distance to move right in units
    private float distanceMoved = 0.0f; // the distance moved so far
    private float xPosition;
    private float customerSpacing = 0.4f;
    private bool isReadyToOrder = false;
    private bool isCustomerInScene = false;
    public Customer customer;
    public UnityEvent OnCustomerDeleted;

    // Start is called before the first frame update
    void Start()
    {
        xPosition = maxDistance - (customerSpacing * DataManager.Instance.spawnedCustomerCount);
        DataManager.Instance.spawnedCustomerCount++;
        DataManager.Instance.customerXPositionTracker.Add(gameObject, xPosition);
        foreach (var item in customer.CustomerOrder) 
        {
            Debug.Log(item.MenuItemName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isCustomerInScene)
        {
            if (distanceMoved < xPosition)
            {
                // Move the gameobject horizontally
                transform.Translate(Vector3.right * speed * Time.deltaTime);

                // Update the distance moved
                distanceMoved += speed * Time.deltaTime;
            }
            if (distanceMoved >= xPosition && !isReadyToOrder)
            {
                //get child item by name on Customer
                var timer = transform.Find("TimerWaitingToOrder");
                Debug.Log("timer name:" + timer.name);
                timer.GetComponent<TimerScript>().readyToBeginTimer = true;
                isReadyToOrder = true;
                isCustomerInScene = true;
            }
        }
    }

    private void OnMouseDown()
    {
        // start taking order. need to add it to a ticket, start the waitingforfoodtimer, and move customer out of scene
        var orderPrefab = transform.Find("prefab_CustomerOrder");
        var orderScript = orderPrefab.GetComponent<OrderController>();

        orderScript.EnableOrder(orderPrefab, customer);

        StartCoroutine(MoveCustomerCoroutine(orderPrefab));
        customer.OrderTaken = true;
        MoveRemainingCustomersInLine(transform.position);
        orderPrefab.SetParent(null);

        var timerScript = transform.Find("TimerWaitingToOrder").GetComponent<TimerScript>();
        timerScript.SetWaitingForFoodTimer();
    }

    private IEnumerator MoveCustomerCoroutine(Transform orderPrefab)
    {
        var orderScript = orderPrefab.GetComponent<OrderController>();

        while (transform.localPosition.x > -4)
        {
            Vector3 newPosition = transform.position + Vector3.left * speed * Time.deltaTime;

            transform.position = newPosition;

            yield return null;

        }
        DataManager.Instance.customerXPositionTracker[gameObject] = transform.position.x;
        orderScript.OnCustomerOutOfSceneFinished(orderPrefab);

        if(!customer.OrderTaken)
        {
            Debug.Log("order not taken");
            //DataManager.Instance.unservedCustomers.Add(customer);
            DataManager.Instance.customerXPositionTracker.Remove(gameObject);
            MoveRemainingCustomersInLine(this.gameObject.transform.position);
            Destroy(this.gameObject);
            OnCustomerDeleted.Invoke();
        }
    }

    public void HandleLeavingCustomerUnserved()
    {
        var orderPrefab = transform.Find("prefab_CustomerOrder");
        StartCoroutine(MoveCustomerCoroutine(orderPrefab));

        Debug.Log("HandleLeavingCustomerUnserved for customer:" + customer.CustomerName);
        customer.OrderTaken = false;
        DataManager.Instance.unservedCustomers.Add(customer);
    }

    public void MoveRemainingCustomersInLine(Vector3 removedCustomerCoordinates)
    {
        Debug.Log("move remaining customers called---");
        Debug.Log("removedCustomerXcoord" + removedCustomerCoordinates.x);
        foreach(KeyValuePair<GameObject,float> customer in DataManager.Instance.customerXPositionTracker)
        {
            Debug.Log("customer position for:" + customer.Key.name + "---" + customer.Key.transform.position.x);
            Debug.Log("customer.Value:" + customer.Value);
            if(customer.Value > removedCustomerCoordinates.x)
            {
                //move them forward
                var newXValue = customer.Key.transform.position.x + customerSpacing;
                customer.Key.transform.position = new Vector3(newXValue, customer.Key.transform.position.y);
                Debug.Log("moving customers");
            }    
        }
    }
}
