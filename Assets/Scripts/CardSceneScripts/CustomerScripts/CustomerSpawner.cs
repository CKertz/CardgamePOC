using Assets.Models;
using Assets.Scripts;
using Assets.Scripts.CardSceneScripts;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using UnityEngine;
using UnityEngine.Events;

public class CustomerSpawner : MonoBehaviour
{
    public UnityEvent customerSpawnEvent;

    public int customerCount;
    public GameObject customerPrefab;
    public float spawnInterval = 15f;
    Vector3 customerSpawnPosition = new Vector3(-4.5f, 2.05f, 0);
    private int spawnedCustomerCount = 0;
    int currentCustomerCount = 0;

    private List<Customer> customerList;
    void Start()
    {

        //construct list of customers to be had that day
        createCustomerList();
        InvokeRepeating("SpawnCustomer", 5f, spawnInterval);

    }

    private void SpawnCustomer()
    {
        if (spawnedCustomerCount >= customerCount)
        {
            CancelInvoke("SpawnCustomer");
        }

        Sprite randomCustomerSprite = Resources.Load<Sprite>(customerList[spawnedCustomerCount].CustomerSpritePath);

        GameObject spawnedCustomer = Instantiate(customerPrefab, transform);
        spawnedCustomer.transform.localPosition = customerSpawnPosition;
        spawnedCustomer.transform.SetParent(transform);
        spawnedCustomer.name = "Customer" + spawnedCustomerCount;

        //customerSpawnEvent.Invoke();

        CustomerController spawnedCustomerController = spawnedCustomer.GetComponent<CustomerController>();
        spawnedCustomerController.customer = customerList[spawnedCustomerCount];

        SpriteRenderer spawnedCustomerSpriteRenderer = spawnedCustomer.GetComponent<SpriteRenderer>();
        spawnedCustomerSpriteRenderer.sprite = randomCustomerSprite;

        spawnedCustomerCount++;
    }


    private void createCustomerList(List<Customer> mandatoryCustomers = null)// "mandatory" meaning force spawning particular people
    {
        customerList = new List<Customer>();

        //first, add mandatory force spawned customers
        if (mandatoryCustomers != null)
        {
            foreach(var customer in mandatoryCustomers)
            {
                customerList.Add(customer);
                currentCustomerCount++;
            }
        }

        //then, add random selection from namedcustomers pool
        string filePath = Application.dataPath + "/Models/json/CustomerNameList.json";
        string json = File.ReadAllText(filePath);
        List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(json);
        AddNamedCustomersToCustomerList(customers);
        //fill the remainder with randos
        for (int i = currentCustomerCount; i < customerCount; i++)
        {
            //create customers with orders
            Customer customer = new Customer();
            customer.CustomerOrder = createRandomCustomerOrder();
            customer.CustomerName = "Rando";//TODO: just a random placeholder for testing
            customer.CustomerSpritePath = "CharacterSprites/randomcharacter"; 
            customerList.Add(customer);
        }

        Randomizer.ShuffleList(customerList);
    }

    private void AddNamedCustomersToCustomerList(List<Customer> availableCustomers)
    {
        //for now, we are just going to spawn half of the customers as named customers, and the rest are random
        while(currentCustomerCount < customerCount / 2)
        {
            //grab random entry out of list, remove from available list after so that a duplicate isn't selected
            Customer randomCustomer = availableCustomers.ElementAt(UnityEngine.Random.Range(0, availableCustomers.Count));
            customerList.Add(randomCustomer);
            availableCustomers.Remove(randomCustomer);
            currentCustomerCount++;
        }
    }

    private List<MenuItem> createRandomCustomerOrder()
    {
        List<MenuItem> customerOrder = new List<MenuItem>();
        
        //flagging system to prevent the order being multiple of the same item
        Dictionary<MenuItem,bool> uniqueItemTracker = new Dictionary<MenuItem,bool>();
        if(DataManager.Instance == null)
        {
            var testingDataManager = FindObjectOfType<DataManager>(true);

            if (testingDataManager != null)
            {
                Debug.Log("TestingDataManager found, setting to active");
                testingDataManager.gameObject.SetActive(true);


                Debug.Log("Empty menu, adding MenuHandler.InitializeTodayMenuItems to DataManager");
                MenuHandler menuHandler = new MenuHandler();
                menuHandler.InitializeTodayMenuItems();
                testingDataManager.gameObject.SetActive(false);

            }
        }
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

            int numberOfItemsToOrder = UnityEngine.Random.Range(1,DataManager.Instance.todayMenuItems.Count);
            for(int i = 0; i < numberOfItemsToOrder; i++)
            {
                int randomMenuItemIndex = UnityEngine.Random.Range(0, DataManager.Instance.todayMenuItems.Count);
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
                    int randomIndex = UnityEngine.Random.Range(0, DataManager.Instance.todayMenuItems.Count - 1);
                    customerOrder.Add(filtered.ElementAt(randomIndex).Key);

                    //make item ineligible to be added again to order
                    uniqueItemTracker[uniqueItemTracker.ElementAt(randomIndex).Key] = false;
                }
            }
        }
        return customerOrder;
    }
}
