using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderControllerScript : MonoBehaviour
{

    private Dictionary<string, int> quantityCounter = new Dictionary<string, int>();

    //public void incrementCount(GameObject cardToProcess)
    //{
    //    //get card name from prefab
    //    //lookup in quantityCounter if that entry exists. increment or create new entry

    //    string cardName = cardToProcess.GetComponent<SetupShopItemData>().childCardName.text;
    //    if(!quantityCounter.ContainsKey(cardName))
    //    {
    //        quantityCounter.Add(cardName, 1);
    //        Debug.Log("new entry. dictionary now contains key of "+cardName+":"+quantityCounter.ContainsKey(cardName));
    //    }
    //    else
    //    {
    //        quantityCounter[cardName] = quantityCounter[cardName] + 1;
    //        Debug.Log("existing entry, " + cardName + " entry now updated to count of:" + quantityCounter[cardName]);
    //    }
    //    updateOrderQuantity(cardName, true);
    //}

    //public void decrementCount(GameObject cardToProcess)
    //{
    //    //get card name from prefab
    //    string cardName = cardToProcess.GetComponent<SetupShopItemData>().childCardName.text;
    //    if (quantityCounter.ContainsKey(cardName))
    //    {
    //        quantityCounter[cardName] = quantityCounter[cardName] - 1;
    //        if(quantityCounter[cardName] == 0)
    //        {
    //            Debug.Log("removing key for" + cardName);
    //            quantityCounter.Remove(cardName);
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("can not decrement count because entry does not exist");
    //    }
    //}

}
