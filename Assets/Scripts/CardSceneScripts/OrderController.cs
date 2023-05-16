using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{


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


}
