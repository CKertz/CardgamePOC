using Assets.Models;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager _instance;
    public static DataManager Instance { get { return _instance; } }
    public int testCounter = 0;
    public Deck deck = new Deck();
    public Dictionary<string, int> quantityCounter = new Dictionary<string, int>();
    public List<MenuItem> todayMenuItems = new List<MenuItem>();

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