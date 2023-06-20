using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{

    public void CalculateOrder(Customer customer)
    {
        Debug.Log("customer "+customer.CustomerName+ " served, yippee 500 points");
    }
}
