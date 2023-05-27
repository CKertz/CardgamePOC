using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    public int orderItems = 3;
    public float speed = 2.0f;
    private bool isEnlarged = false;
    private void OnMouseDown()
    {
        Debug.Log("clicked");
        if(isEnlarged)
        {
            transform.localScale = new Vector3(0.25f, 0.25f);
            isEnlarged = false;
        }
        else
        {
            transform.localScale = new Vector3(1, 1f);
            isEnlarged = true;
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
        Debug.Log(orderPrefab.name);
        orderPrefab.SetParent(null);
        StartCoroutine(MoveOrderCoroutine(orderPrefab));
    }

    private IEnumerator MoveOrderCoroutine(Transform orderPrefab)
    {
        Debug.Log(orderPrefab.name + ", x="+ orderPrefab.position.x);
        while (orderPrefab.transform.position.x < 1f)
        {
            Vector3 newPosition = orderPrefab.position + Vector3.right * speed * Time.deltaTime * 1.2f;
            orderPrefab.transform.position = newPosition;
            Debug.Log(orderPrefab.position.x);
            yield return null;

        }
        OnOrderOutOfSceneFinished(orderPrefab);
    }

    private void OnOrderOutOfSceneFinished(Transform orderPrefab)
    {
        orderPrefab.localScale = new Vector3(0.25f, 0.25f);
        isEnlarged = false;
        orderPrefab.localPosition = new Vector3(-1f, 0.43f);
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

    public void EnableOrder(Transform orderPrefab, Customer customer)
    {
        var orderBackgroundSprite = transform.GetComponent<SpriteRenderer>();

        orderBackgroundSprite.enabled = true;
        orderBackgroundSprite.transform.localPosition = new Vector3(1, 0.1f);

        var orderItemSprites = GetOrderItemSprites(customer);
        LoadOrderItemSprites(orderItemSprites, transform);
    }

}
