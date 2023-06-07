using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatedDishController : MonoBehaviour
{

    void Start()
    {

    }

    public void EnableDishIngredientSpriteByCardName(string cardName)
    {
        var ingredientToActivate = transform.Find(cardName).gameObject;
        ingredientToActivate.GetComponent<SpriteRenderer>().enabled = true;
    }
}
