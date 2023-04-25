using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopulateBillPanel : MonoBehaviour
{
    public GameObject billPanel;
    public GameObject childPanelPrefab;

    public void incrementCount(GameObject cardToProcess, Dictionary<string,int> quantityCounter)
    {
        //get card name from prefab
        //lookup in quantityCounter if that entry exists. increment or create new entry

        string cardName = cardToProcess.GetComponent<SetupShopItemData>().childCardName.text;
        if (!quantityCounter.ContainsKey(cardName))
        {
            quantityCounter.Add(cardName, 1);
            //Debug.Log("new entry. dictionary now contains key of " + cardName + ":" + quantityCounter.ContainsKey(cardName));
        }
        else
        {
            quantityCounter[cardName] = quantityCounter[cardName] + 1;
            //Debug.Log("existing entry, " + cardName + " entry now updated to count of:" + quantityCounter[cardName]);
        }

        GameObject billItemToUpdate = GameObject.Find(cardName);
        SetupBillItemData billItem = billItemToUpdate.GetComponent<SetupBillItemData>();
        billItem.childCardQuantity.text = "x" + quantityCounter[cardName];

    }

}
