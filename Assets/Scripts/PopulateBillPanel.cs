using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopulateBillPanel : MonoBehaviour
{
    public GameObject billPanel;
    public GameObject childPanelPrefab; 

    void Start()
    {
        //billPanel = GameObject.Find("Bill Item Container");
        //if (1 == 1)
        //{
        //    for (int i = 0; i < 1; i++)
        //    {
        //        AddChildPanel();
        //    }
        //}

    }

    void Update()
    {
        
    }
    void AddChildPanel()
    {
        // Instantiate the child Panel prefab
        GameObject childPanel = Instantiate(childPanelPrefab);
        // Set the parent of the child Panel to be the parent Panel
        childPanel.transform.SetParent(billPanel.transform);
    }

}
