using Assets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CardSceneScripts
{
    public class PlayCardHandler
    {
        //public void HandleIngredientCardPlayed(string cardName, string associatedRecipePrefabPath, GameObject cardObject)
        //{
        //    Debug.Log("HandleIngredientCardPlayed called. cardname" + cardName);
        //    var dishToEdit = GetFirstDishAvailableWithAssociatedRecipe(cardName, associatedRecipePrefabPath);
        //    if (DataManager.Instance.activeDishes.Count >= 5 && dishToEdit == null)
        //    {
        //        Debug.LogWarning("There is no room for this card to be played");
        //        return;
        //    }

        //    if (dishToEdit == null)
        //    {
        //        //there is room to make a new dish and we are making a fresh one, not contributing to an existing one
        //        var dishSpawner = GameObject.Find("DishSpawner").GetComponent<DishSpawner>();
        //        var emptyDish = dishSpawner.InstantiateDish();

        //        var newDish = dishSpawner.InstantiateFoodPrefab(associatedRecipePrefabPath, emptyDish);
        //        var platedDishController = newDish.GetComponentInChildren<PlatedDishController>();
        //        platedDishController.EnableDishIngredientSpriteByCardName(cardName);

        //        DataManager.Instance.activeDishes.Add(new PlatedDish(associatedRecipePrefabPath, false, newDish));

        //    }
        //    else
        //    {

        //    }



        //}
    }
}
