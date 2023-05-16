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

        StartCoroutine(MoveCustomerCoroutine());
        orderPrefab.SetParent(null);
    }

    private IEnumerator MoveCustomerCoroutine()
    {
        var orderPrefab = transform.Find("prefab_CustomerOrder");
        while (transform.localPosition.x > -4)
        {
            Vector3 newPosition = transform.position + Vector3.left * speed * Time.deltaTime;

            transform.position = newPosition;

            yield return null;

        }
        OnCustomerOutOfSceneFinished(orderPrefab);
    }

    private void OnCustomerOutOfSceneFinished(Transform orderPrefab)
    {
        StartCoroutine(MoveOrderCoroutine(orderPrefab));
    }

    private IEnumerator MoveOrderCoroutine(Transform orderPrefab)
    {     
        while (orderPrefab.transform.localPosition.x < -2f)
        {
            Vector3 newPosition = orderPrefab.position + Vector3.right * speed * Time.deltaTime * 1.2f;
            orderPrefab.transform.position = newPosition;

            yield return null;

        }
        OnOrderOutOfSceneFinished(orderPrefab);
    }
    private void OnOrderOutOfSceneFinished(Transform orderPrefab)
    {
        orderPrefab.localScale = new Vector3(0.25f, 0.25f);
        orderPrefab.localPosition = new Vector3(-2.04f, 0.43f);
        var background = orderPrefab.transform.Find("OrderBackground");
        var backgroundSprite = background.GetComponent<SpriteRenderer>();
        backgroundSprite.sortingOrder = 2;

        AdjustOrderItemIconSortingOrder(background);
    }
    private void AdjustOrderItemIconSortingOrder(Transform orderPrefab)
    {
        for(int i = 0; i < customer.CustomerOrder.Count; i++)
        {
            var currentOrderItem = orderPrefab.transform.Find("OrderItem" + i).GetComponent<SpriteRenderer>();
            currentOrderItem.sortingOrder = 2;
        }
    }

}
