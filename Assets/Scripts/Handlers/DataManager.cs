using Assets.Models;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager _instance;
    public static DataManager Instance { get { return _instance; } }
    public Deck deck = new Deck();
    public Dictionary<string, int> quantityCounter = new Dictionary<string, int>();
    public List<MenuItem> todayMenuItems = new List<MenuItem>();
    public int acceptedOrderCount = 0;
    public List<Customer> servedCustomers = new List<Customer>();
    public List<Customer> unservedCustomers = new List<Customer>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}