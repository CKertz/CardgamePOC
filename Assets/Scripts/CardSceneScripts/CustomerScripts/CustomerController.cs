using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CustomerController : MonoBehaviour
{
    public float speed = 2.0f;
    public float maxDistance = 5.0f; // the maximum distance to move right in units
    private float distanceMoved = 0.0f; // the distance moved so far
    private bool isReadyToOrder = false;
    private bool isCustomerInScene = false;
    public Customer customer;

    // Start is called before the first frame update
    void Start()
    {
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
            if (distanceMoved < maxDistance)
            {
                // Move the gameobject horizontally
                transform.Translate(Vector3.right * speed * Time.deltaTime);

                // Update the distance moved
                distanceMoved += speed * Time.deltaTime;
            }
            if (distanceMoved >= maxDistance && !isReadyToOrder)
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
        orderScript.OnCustomerOutOfSceneFinished(orderPrefab);
    }
}
