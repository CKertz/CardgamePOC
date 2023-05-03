using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    public float speed = 2.0f;
    public float maxDistance = 5.0f; // the maximum distance to move right in units
    private float distanceMoved = 0.0f; // the distance moved so far
    public CustomerPatienceScript customerPatiencePrefab;
    private bool isReadyToOrder = true;
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
        if (distanceMoved >= maxDistance && isReadyToOrder)
        {
            customerPatiencePrefab.StartTimer(TimerType.OrderTakenPatience);
            isReadyToOrder=false;
        }
    }


}
