using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManagerScript : MonoBehaviour
{
    public GameObject timerPrefab;

    public void AddNewTimer(GameObject timer)
    {
        Debug.Log("instantiating now");
        Instantiate(timerPrefab);
    }
}
