using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopulateBillPanel : MonoBehaviour
{
    public GameObject billPanel;
    public GameObject childPanelPrefab;
    public Dictionary<string, int> quantityCounterOLD = new Dictionary<string, int>();

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


    public void incrementCount(GameObject cardToProcess, Dictionary<string,int> quantityCounter)
    {
        //get card name from prefab
        //lookup in quantityCounter if that entry exists. increment or create new entry

        string cardName = cardToProcess.GetComponent<SetupShopItemData>().childCardName.text;
        if (!quantityCounter.ContainsKey(cardName))
        {
            quantityCounter.Add(cardName, 1);
            Debug.Log("new entry. dictionary now contains key of " + cardName + ":" + quantityCounter.ContainsKey(cardName));
        }
        else
        {
            quantityCounter[cardName] = quantityCounter[cardName] + 1;
            Debug.Log("existing entry, " + cardName + " entry now updated to count of:" + quantityCounter[cardName]);
        }
        updateOrderQuantity(cardName, true);
    }

    //public void decrementCount(GameObject cardToProcess)
    //{
    //    //get card name from prefab
    //    string cardName = cardToProcess.GetComponent<SetupShopItemData>().childCardName.text;
    //    if (quantityCounterOLD.ContainsKey(cardName))
    //    {
    //        quantityCounterOLD[cardName] = quantityCounterOLD[cardName] - 1;
    //        if (quantityCounterOLD[cardName] == 0)
    //        {
    //            Debug.Log("removing key for" + cardName);
    //            quantityCounterOLD.Remove(cardName);
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("can not decrement count because entry does not exist");
    //    }
    //}

    public void updateOrderQuantity(string cardName, bool isIncrementing)
    {
        // make a call to this in the increment/decrement
        // find the bill prefab with cardName
        // use isincrementing to give bill prefab quantity +/- 1
        // if count is brought from 1 to 0, remove bill item
        // if count is > 1, don't instantiate a bill item prefab
    }

}
