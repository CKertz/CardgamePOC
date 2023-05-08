using Assets.Models;
using Assets.Scripts.CardSceneScripts;
using System.Collections;
using System.Collections.Generic;
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
    public GameObject timerPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 15f;
    Vector3 position = new Vector3(-32, 0, 0);
    private int spawnedCustomerCount = 0;

    private List<Customer> customerList;
    void Start()
    {

        //construct list of customers to be had that day
        createCustomerList(customerCount);
        InvokeRepeating("SpawnCustomer", 5f, spawnInterval);

    }


    void Update()
    {
        
    }


    void spawnPatienceTimer(GameObject parentObject)
    {
        GameObject customerPatienceTimerObject = Instantiate(timerPrefab);
        CustomerPatienceScript timer = customerPatienceTimerObject.GetComponent<CustomerPatienceScript>();
        customerPatienceTimerObject.transform.SetParent(parentObject.transform);
    }

    private void SpawnCustomer()
    {
        if (spawnedCustomerCount >= customerCount)
        {
            CancelInvoke("SpawnCustomer");
        }

        Sprite randomCustomerSprite = Resources.Load<Sprite>(customerList[spawnedCustomerCount].CustomerSpritePath);

        GameObject spawnedCustomer = Instantiate(customerPrefab, position, Quaternion.identity);
        spawnedCustomer.transform.SetParent(transform);

        customerSpawnEvent.Invoke();

        CustomerController spawnedCustomerController = spawnedCustomer.GetComponent<CustomerController>();
        spawnedCustomerController.customer = customerList[spawnedCustomerCount];

        SpriteRenderer spawnedCustomerSpriteRenderer = spawnedCustomer.GetComponent<SpriteRenderer>();
        spawnedCustomerSpriteRenderer.sprite = randomCustomerSprite;
        //spawnPatienceTimer(spawnedCustomer);
        //add customer's order from customerlist to customer somehow?
        spawnedCustomerCount++;
    }


    private void createCustomerList(int providedCustomerCount, List<Customer> mandatoryCustomers = null)// "mandatory" meaning force spawning particular people
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
            customer.CustomerSpritePath = "CharacterSprites/character1"; //TODO: just a random placeholder for testing
            customerList.Add(customer);
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
