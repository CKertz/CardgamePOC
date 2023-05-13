using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class TimerScript : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    public float currentTime;
    [Header("Limit Settings")]
    public bool hasLimit;

    public float timerLimit;
    public bool countdown;
    private bool isFirstCall = true;
    public bool readyToBeginTimer = false;

    void Start()
    {
        GameObject timerManager = GameObject.FindGameObjectWithTag("DebugOverlayUI");
        timerText.transform.SetParent(timerManager.transform);
        Debug.Log("On Start, readyToBeginTimer:"+readyToBeginTimer);
    }

    // Update is called once per frame
    void Update()
    {
        if(readyToBeginTimer)
        {
            currentTime = countdown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;
            if(hasLimit && ((countdown && currentTime <= timerLimit) || (!countdown && currentTime >= timerLimit)))
            {
                currentTime = timerLimit;
                SetTimerText();
                timerText.color = Color.red;
                enabled = false;
            }
            SetTimerText();
        }

    }

    private void SetTimerText()
    {
        if(isFirstCall)
        {
            isFirstCall = false;
        }
        timerText.text = currentTime.ToString("0.00");
    }


}
