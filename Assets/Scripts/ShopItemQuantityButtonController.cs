using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopItemQuantityButtonController : MonoBehaviour
{
    public UnityEvent OnPlusClick;

    public void OnPlusButtonClick()
    {
        // Code to be executed when the button is clicked
        //update respective shop item with running count, event/delegates for order items?
        // https://youtu.be/OuZrhykVytg?t=401 
        OnPlusClick?.Invoke();
    }

    public void TestMethod()
    {
        Debug.Log("test method hit");
    }
    ////
    //private void Start()
    //{
    //    // Registering an event handler to the OnPlusClick event
    //    OnPlusClick += HandleOnPlusClick;
    //}
    //private void HandleOnPlusClick(object sender, EventArgs e)
    //{
    //    // Handle the event here
    //    Debug.Log("OnPlusClick event handled");
    //}
    ////
    public void OnMinusButtonClick()
    {
        // Code to be executed when the button is clicked
        Debug.Log("Minus Button clicked!");
    }
}
