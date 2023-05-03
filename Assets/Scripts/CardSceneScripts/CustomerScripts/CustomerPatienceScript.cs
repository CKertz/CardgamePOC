using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPatienceScript : MonoBehaviour
{
    private float countdownTime;
    public float orderTakenPatienceTimer = 30f;
    public float orderCompletedPatienceTimer = 60f;

    private void setTimerDuration(TimerType timerType)
    {
        switch (timerType)
        {
            case TimerType.OrderCompletionPatience:
                countdownTime = orderCompletedPatienceTimer;
                break;
            case TimerType.OrderTakenPatience:
                countdownTime = orderTakenPatienceTimer;
                break;
            default:
                Debug.Log("timer type not specified");
                break;
        }
    }

    public void StartTimer(TimerType timerType)
    {
        setTimerDuration(timerType);
        Debug.Log("countdown timer made for:" + countdownTime + " seconds");

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
        DataManager.Instance.testCounter++;

    }
}
