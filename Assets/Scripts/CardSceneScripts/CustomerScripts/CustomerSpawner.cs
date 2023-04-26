using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    private List<Customer> customerList;
    public int customerCount;
    // Start is called before the first frame update
    void Start()
    {
        //construct list of customers to be had that day
        createCustomerList(customerCount);
        //start timer
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<Customer> createCustomerList(int providedCustomerCount, List<Customer> mandatoryCustomers = null)// "mandatory" meaning force spawning particular people
    {
        customerList = new List<Customer>();

        int currentCustomerCount = 0;
        if (mandatoryCustomers != null)
        {
            foreach(var customer in mandatoryCustomers)
            {
                customerList.Add(customer);
                currentCustomerCount++;
            }
        }

        for(int i = currentCustomerCount; i < providedCustomerCount; i++)
        {
            Debug.Log("customer number " + i + " now being created");
            //create customers with orders
            Customer customer = new Customer();
            customer.CustomerOrder = createRandomCustomerOrder();
            customer.CustomerName = "testname";
            customer.CustomerSpritePath = "testpath";
        }

        return customerList;
    }

    private List<MenuItem> createRandomCustomerOrder()
    {
        List<MenuItem> customerOrder = new List<MenuItem>();
        
        //flagging system to prevent the order being multiple of the same item
        Dictionary<MenuItem,bool> uniqueItemTracker = new Dictionary<MenuItem,bool>();
        foreach(MenuItem item in DataManager.Instance.todayMenuItems)
        {
            uniqueItemTracker.Add(item,true);
            //Debug.Log(item.MenuItemName + " added to tracker");
        }

        if(DataManager.Instance.todayMenuItems != null)
        {
            //temp logging
            foreach(MenuItem menuItem in DataManager.Instance.todayMenuItems)
            {
                //Debug.Log(menuItem.MenuItemName);
            }

            int numberOfItemsToOrder = Random.Range(1,DataManager.Instance.todayMenuItems.Count);
            for(int i = 0; i < numberOfItemsToOrder; i++)
            {
                int randomMenuItemIndex = Random.Range(0, DataManager.Instance.todayMenuItems.Count);
                //Debug.Log("randomMenuItemIndex:" + randomMenuItemIndex);

                if (uniqueItemTracker.ElementAt(randomMenuItemIndex).Value == true)
                {
                    Debug.Log("adding " + uniqueItemTracker.ElementAt(randomMenuItemIndex).Key.MenuItemName + " to order");
                    Debug.Log("this is item #" + (i + 1) + " on this order");
                    customerOrder.Add(uniqueItemTracker.ElementAt(randomMenuItemIndex).Key);
                    
                    //make item ineligible to be added again to order
                    uniqueItemTracker[uniqueItemTracker.ElementAt(randomMenuItemIndex).Key] = false;
                }
                else
                {
                    //TODO: I think we can just remove the if and refactor this to work in all cases?
                    IEnumerable<KeyValuePair<MenuItem, bool>> filtered = uniqueItemTracker.Where(pair => pair.Value == true);
                    int randomIndex = Random.Range(0, DataManager.Instance.todayMenuItems.Count - 1);
                    customerOrder.Add(filtered.ElementAt(randomIndex).Key);

                    //make item ineligible to be added again to order
                    uniqueItemTracker[uniqueItemTracker.ElementAt(randomIndex).Key] = false;
                }
            }
        }
        return customerOrder;
    }
}
