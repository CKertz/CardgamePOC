using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CustomerController : MonoBehaviour
{
    public float speed = 2.0f;
    public float maxDistance = 5.0f; // the maximum distance to move right in units
    private float distanceMoved = 0.0f; // the distance moved so far
    private bool isReadyToOrder = false;
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
            isReadyToOrder=true;
        }
    }

    private void OnMouseDown()
    {
        // start taking order
        // start waitingforfood timer
        var orderPrefab = transform.Find("prefab_CustomerOrder");
        var orderBackgroundObject = orderPrefab.transform.Find("OrderBackground");
        var orderBackgroundSprite = orderBackgroundObject.GetComponent<SpriteRenderer>();

        orderBackgroundSprite.enabled = true;
        orderBackgroundSprite.transform.localPosition = new Vector3(4, 0.1f);


        var orderScript = orderPrefab.GetComponent<OrderController>();
        var orderItemSprites = new List<string>();
        orderItemSprites = orderScript.GetOrderItemSprites(customer);
        
        for(int i = 0; i < orderItemSprites.Count; i++)
        {
            Debug.Log("OrderItem" + i);
            var currentOrderItem = orderBackgroundObject.transform.Find("OrderItem" + i).GetComponent<SpriteRenderer>();
            currentOrderItem.enabled = true;

            Debug.Log("currentorderitem:" + currentOrderItem.name);
            if (currentOrderItem != null)
            {
                Debug.Log("spritepath:"+orderItemSprites[i]);
                Sprite sprite = Resources.Load<Sprite>(orderItemSprites[i]);
                currentOrderItem.sprite = sprite;

            }
        }

    }


}
