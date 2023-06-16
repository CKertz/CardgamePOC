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
    public UnityEvent OnCustomerOrderTaken;

    // Start is called before the first frame update
    void Start()
    {
        xPosition = maxDistance - (customerSpacing * DataManager.Instance.currentCustomerInLineCount);
        DataManager.Instance.currentCustomerInLineCount++;
        DataManager.Instance.customerXPositionTracker.Add(gameObject, xPosition);

        // check who is front of line and make them clickable
        HandleFrontOfLineCustomerBoxCollider();

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
                timer.GetComponent<TimerScript>().readyToBeginTimer = true;
                isReadyToOrder = true;
                isCustomerInScene = true;
            }
        }
    }

    public void HandleFrontOfLineCustomerBoxCollider()
    {
        var customersInLine = GameObject.FindGameObjectsWithTag("Customer");
        if(customersInLine.Length == 0)
        {
            return;
        }

        GameObject frontOfLineCustomer = null;
        float maxValue = -999f;
        foreach (var lineCustomer in customersInLine)
        {
            Debug.Log("customers in line:" + lineCustomer.name);
            if (lineCustomer.gameObject.transform.position.x > maxValue)
            {
                frontOfLineCustomer = lineCustomer;
                maxValue = lineCustomer.gameObject.transform.position.x;
            }
        }
        Debug.Log("CUSTOMER:"+frontOfLineCustomer.gameObject.name +" IS FRONT OF THE LINE");
        frontOfLineCustomer.GetComponent<BoxCollider2D>().enabled = true;
        frontOfLineCustomer.GetComponent<CustomerController>().customer.IsFrontOfLine = true;

    }

    private void OnMouseDown()
    {
        OnCustomerOrderTaken.Invoke();
        // start taking order. need to add it to a ticket, start the waitingforfoodtimer, and move customer out of scene
        var orderPrefab = transform.Find("prefab_CustomerOrder");
        var orderScript = orderPrefab.GetComponent<OrderController>();
        orderScript.StartOrderCompletionListener(customer);

        orderScript.EnableOrder(customer);

        StartCoroutine(MoveCustomerCoroutine(orderPrefab));
        customer.OrderTaken = true;
        MoveRemainingCustomersInLine(transform.position);
        orderPrefab.SetParent(null);

        var timerScript = transform.Find("TimerWaitingToOrder").GetComponent<TimerScript>();
        timerScript.SetWaitingForFoodTimer();

        DataManager.Instance.currentCustomerInLineCount--;
        DataManager.Instance.customerXPositionTracker.Remove(gameObject);
        gameObject.tag = "Customer_OrderTaken";

        // check who is front of line and make them clickable
        HandleFrontOfLineCustomerBoxCollider();
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
        DataManager.Instance.currentCustomerInLineCount--;

        // check who is front of line and make them clickable
        HandleFrontOfLineCustomerBoxCollider();
    }

    public void MoveRemainingCustomersInLine(Vector3 removedCustomerCoordinates)
    {
        foreach(KeyValuePair<GameObject,float> customer in DataManager.Instance.customerXPositionTracker)
        {
            if(customer.Value > removedCustomerCoordinates.x)
            {
                //move them forward
                var newXValue = customer.Key.transform.position.x + customerSpacing;
                customer.Key.transform.position = new Vector3(newXValue, customer.Key.transform.position.y);
                Debug.Log("moving customers");
            }    
        }

        // check who is front of line and make them clickable
        HandleFrontOfLineCustomerBoxCollider();
    }
}
