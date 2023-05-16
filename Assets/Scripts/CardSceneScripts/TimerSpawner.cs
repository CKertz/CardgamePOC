using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSpawner : MonoBehaviour
{
    public GameObject timerWaitingForFoodPrefab;
    public GameObject timerWaitingToOrderPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnWaitingForFoodTimer()
    {
        Instantiate(timerWaitingForFoodPrefab);
    }

    public void SpawnWaitingToOrderTimer()
    {
        Instantiate(timerWaitingToOrderPrefab);
        TimerScript timerScript = timerWaitingToOrderPrefab.GetComponent<TimerScript>();
        timerScript.readyToBeginTimer = true;
        Debug.Log("SpawnWaitingForOrderTimer hit, readyToBeginTimer:" + timerScript.readyToBeginTimer);
    }

}
