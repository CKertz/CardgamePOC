using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderControllerScript : MonoBehaviour
{
    private int bunCount = 0;
    private int cheeseCount = 0;
    private int pattyCount = 0;

    private Dictionary<string, int> quantityCounter = new Dictionary<string, int>();

    public void incrementCount(GameObject cardToProcess)
    {
        //get card name from prefab
        //lookup in quantityCounter if that entry exists. increment or create new entry
        //TODO: the wiring of this may not work. made a prefab of empty gameobject and attached the script to it so that onclick increment button would work


    }

    public void deccrementCount(GameObject cardToProcess)
    {
        //get card name from prefab

    }
}
