using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    //TODO: use a weighted system to give a cash reward/tip based off of skills, gear used, etc

    public void CalculateOrder(Customer customer)
    {
        float orderCost = 0;

        foreach(var orderItem in customer.CustomerOrder)
        {
            orderCost += orderItem.MenuItemPrice;
        }

        DataManager.Instance.totalDailyEarnings += orderCost;
        Debug.Log("customer order cost: " + orderCost);
        Debug.Log("total daily earnings:" + DataManager.Instance.totalDailyEarnings);
    }
}
