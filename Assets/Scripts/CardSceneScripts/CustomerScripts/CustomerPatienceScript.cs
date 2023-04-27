using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPatienceScript : MonoBehaviour
{
    public float countdownTime = 10f;

    void Start()
    {
        // Set the initial value of countdownTime to 60 seconds
        countdownTime = 10f;
    }

    public void StartTimer()
    {
        InvokeRepeating("UpdateTimer", 1f, 1f);
    }

    void UpdateTimer()
    {
        countdownTime--;
        Debug.Log(countdownTime);
        if (countdownTime <= 0)
        {
            CancelInvoke("UpdateTimer");
            Debug.Log("Time's up!");
        }
    }
}
