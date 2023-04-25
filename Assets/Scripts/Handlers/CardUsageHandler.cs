using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUsageHandler : MonoBehaviour
{
    private float lastClickTime = 0f;
    private float doubleClickTimeThreshold = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseUpAsButton()
    {
        if (Time.time - lastClickTime < doubleClickTimeThreshold)
        {
            // Perform action on double-click
            Debug.Log("Sprite double-clicked!");
            //attempt to use card toward an active order
            //if successful, consume card, perform actions
            //gameObject.GetComponent ...
            //Destroy(gameObject);
        }
        lastClickTime = Time.time;
    }
}
