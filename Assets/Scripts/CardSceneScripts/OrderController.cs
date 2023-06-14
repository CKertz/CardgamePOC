using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    public int orderItems = 3;
    public float speed = 2.0f;
    public bool isOnSpike = false;
    private Vector3 mousePostiionOffset;
    private float ticketRackOriginalXCoordinate;
    private float ticketRackOriginalYCoordinate = 0.43f;
    private Customer customer;

    private void OnMouseOver()
    {
        if(!isOnSpike)
        {
            transform.localScale = new Vector3(0.75f, 0.75f);
        }

    }

    private void OnMouseExit()
    {
        transform.localScale = new Vector3(0.25f, 0.25f);
    }

    private Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        mousePostiionOffset = gameObject.transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + mousePostiionOffset;
    }

    private void OnMouseUp()
    {
        Debug.Log("onmouse up, isonspike:" + isOnSpike);
        if(isOnSpike)
        {
            gameObject.transform.localScale = new Vector3(0.1f, 0.1f);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Debug.Log("customer name test:" + customer.CustomerName);
            //trigger a scan over all items in window and attempt to fulfill order
        }
        else
        {
            //snap back to original location
            transform.localPosition = new Vector3(ticketRackOriginalXCoordinate, ticketRackOriginalYCoordinate);

        }
    }

    public List<string> GetOrderItemSprites(Customer customer)
    {
        var customerOrderItemSprites = new List<string>();
        foreach(MenuItem menuItem in customer.CustomerOrder)
        {
            customerOrderItemSprites.Add(menuItem.MenuItemSpritePath);
        }
        return customerOrderItemSprites;
    }

    public void LoadOrderItemSprites(List<string> orderItemSprites, Transform orderBackgroundObject)
    {
        for (int i = 0; i < orderItemSprites.Count; i++)
        {
            var currentOrderItem = orderBackgroundObject.transform.Find("OrderItem" + i).GetComponent<SpriteRenderer>();
            currentOrderItem.enabled = true;

            if (currentOrderItem != null)
            {
                Sprite sprite = Resources.Load<Sprite>(orderItemSprites[i]);
                currentOrderItem.sprite = sprite;

            }
        }
    }
    public void OnCustomerOutOfSceneFinished(Transform orderPrefab)
    {
        orderPrefab.SetParent(null);
        StartCoroutine(MoveOrderCoroutine(orderPrefab));
    }

    private IEnumerator MoveOrderCoroutine(Transform orderPrefab)
    {
        while (orderPrefab.transform.position.x < 1f)
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
        transform.GetComponent<BoxCollider2D>().enabled = true;
        DataManager.Instance.acceptedOrderCount++;
        var xPosition = -0.8f + (0.25f * DataManager.Instance.acceptedOrderCount);
        Debug.Log("order count:" + DataManager.Instance.acceptedOrderCount + ",x position:"+xPosition);
        ticketRackOriginalXCoordinate = xPosition;
        orderPrefab.localPosition = new Vector3(xPosition, 0.43f);
        var backgroundSprite = GetComponent<SpriteRenderer>();
        backgroundSprite.sortingOrder = 2;

        AdjustOrderItemIconSortingOrder(transform);
    }
    private void AdjustOrderItemIconSortingOrder(Transform orderPrefab)
    {
        for (int i = 0; i < orderItems; i++)
        {
            var currentOrderItem = orderPrefab.transform.Find("OrderItem" + i).GetComponent<SpriteRenderer>();
            currentOrderItem.sortingOrder = 3;
        }
    }

    public void EnableOrder(Customer givenCustomer)
    {
        var orderBackgroundSprite = transform.GetComponent<SpriteRenderer>();

        orderBackgroundSprite.enabled = true;
        orderBackgroundSprite.transform.localPosition = new Vector3(1, 0.1f);

        var orderItemSprites = GetOrderItemSprites(givenCustomer);
        customer = givenCustomer;
        LoadOrderItemSprites(orderItemSprites, transform);
    }

    public void RemoveOrder()
    {
        Destroy(this.gameObject);
    }
}
