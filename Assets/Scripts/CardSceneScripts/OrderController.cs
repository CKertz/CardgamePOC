using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    public int orderItems = 3;
    public float speed = 2.0f;

    public List<string> GetOrderItemSprites(Customer customer)
    {
        var customerOrderItemSprites = new List<string>();
        foreach(MenuItem menuItem in customer.CustomerOrder)
        {
            customerOrderItemSprites.Add(menuItem.MenuItemSpritePath);
            Debug.Log("sprite item path: " +  menuItem.MenuItemSpritePath);
        }
        return customerOrderItemSprites;
    }

    public void LoadOrderItemSprites(List<string> orderItemSprites, Transform orderBackgroundObject)
    {
        for (int i = 0; i < orderItemSprites.Count; i++)
        {
            Debug.Log("OrderItem" + i);
            var currentOrderItem = orderBackgroundObject.transform.Find("OrderItem" + i).GetComponent<SpriteRenderer>();
            currentOrderItem.enabled = true;

            Debug.Log("currentorderitem:" + currentOrderItem.name);
            if (currentOrderItem != null)
            {
                Debug.Log("spritepath:" + orderItemSprites[i]);
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
        while (orderPrefab.transform.localPosition.x < -5f)
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
        for (int i = 0; i < orderItems; i++)
        {
            var currentOrderItem = orderPrefab.transform.Find("OrderItem" + i).GetComponent<SpriteRenderer>();
            currentOrderItem.sortingOrder = 3;
        }
    }

    public void EnableOrder(Transform orderPrefab, Customer customer)
    {
        var orderBackgroundObject = orderPrefab.transform.Find("OrderBackground");
        var orderBackgroundSprite = orderBackgroundObject.GetComponent<SpriteRenderer>();

        orderBackgroundSprite.enabled = true;
        orderBackgroundSprite.transform.localPosition = new Vector3(4, 0.1f);

        var orderItemSprites = GetOrderItemSprites(customer);
        LoadOrderItemSprites(orderItemSprites, orderBackgroundObject);
    }

}
