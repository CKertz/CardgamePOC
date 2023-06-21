using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishSpawner : MonoBehaviour
{
    public GameObject dishPrefab;
    private float dishXPosition = -1;
    private float dishYPosition = -0.2f;
    private float dishSpacing = 0.5f;

    public bool AttemptToInstantiateDish(string cardName, string associatedRecipePrefabPath, GameObject cardObject)
    {
        Debug.Log("AttemptToInstantiateDish called. cardname" + cardName);
        var availableDishList = GetAllDishesAvailableWithAssociatedRecipe(cardName, associatedRecipePrefabPath);
        if (DataManager.Instance.activeDishes.Count >= 5 && availableDishList.Count == 0 )
        {
            Debug.LogWarning("There is no room for this card to be played. activedish.count="+ DataManager.Instance.activeDishes.Count + " availabeDishList count:"+availableDishList.Count);
            return false;
        }

        if (availableDishList.Count == 0 && DataManager.Instance.activeDishes.Count < 5)
        {
            Debug.Log("availabledishlist is 0 and activedishes < 5");
            //there is room to make a new dish and we are making a fresh one, not contributing to an existing one
            var emptyDish = InstantiateDish();
            var emptyDishController = emptyDish.GetComponent<DishController>();

            var newDish = InstantiateFoodPrefab(associatedRecipePrefabPath, emptyDish);
            var platedDishController = newDish.GetComponentInChildren<PlatedDishController>();
            platedDishController.EnableDishIngredientSpriteByCardName(cardName);
            // check if dish is done (1 card orders)
            var dishID = DataManager.Instance.dishIDCounter;
            Debug.Log("dishID about to be added:" + dishID);
            if (IsDishComplete(newDish))
            {
                var platedDish = new PlatedDish(associatedRecipePrefabPath, isCompleted: true, newDish, dishID);
                DataManager.Instance.activeDishes.Add(platedDish);
                emptyDishController.platedDish = platedDish;
            }
            else
            {
                var platedDish = new PlatedDish(associatedRecipePrefabPath, isCompleted: false, newDish, dishID);
                DataManager.Instance.activeDishes.Add(platedDish);
                emptyDishController.platedDish = platedDish;
            }
            DataManager.Instance.dishIDCounter++;
        }
        else
        {
            //get active dish with same recipe and !iscompleted
            //check if sprite can be enabled for this card
            foreach (var dish in availableDishList)
            {
                Debug.Log("checking dish in dishlist: " + dish.DishPrefabName);
                if(!IsIngredientSpriteActiveForDish(cardName, dish.PlatedDishObject))
                {
                    Debug.Log("sprite:"+cardName+ " is able to activate for dish. attempting to enable now.");
                    var platedDishController = dish.PlatedDishObject.GetComponentInChildren<PlatedDishController>();
                    platedDishController.EnableDishIngredientSpriteByCardName(cardName);

                    if (IsDishComplete(dish.PlatedDishObject))
                    {
                        dish.IsCompleted = true;
                    }
                    break;
                }
            }
        }
        return true;
    }

    private GameObject InstantiateDish()
    {
        //handle transform positioning (lines of multiple dishes)
        //handle passing on correct food prefab to dishcontroller
        var adjustedSpawnPosition = CalculateDishSpawnPosition();
        GameObject dish = Instantiate(dishPrefab, adjustedSpawnPosition, Quaternion.identity);
        DataManager.Instance.spawnedDishCount++;
        return dish;
    }

    private GameObject InstantiateFoodPrefab(string associatedRecipe, GameObject emptyDish)
    {
        GameObject associatedRecipePrefab = Resources.Load<GameObject>(associatedRecipe);
        if (associatedRecipePrefab == null )
        {
            Debug.Log("AssociatedRecipe prefab not found: " + associatedRecipe);
            return null;
        }
        GameObject spawnedPrefab = Instantiate(associatedRecipePrefab);
        spawnedPrefab.transform.SetParent(emptyDish.transform, false);
        return spawnedPrefab;
    }

    private Vector3 CalculateDishSpawnPosition()
    {
        float xPos = dishXPosition + (DataManager.Instance.spawnedDishCount * dishSpacing);
        return new Vector3(xPos, dishYPosition, 0);
    }

    //scan all active dishes and find first one (if any) that has the ability to play the given card in it
    public GameObject GetFirstDishAvailableWithAssociatedRecipe(string cardName, string associatedRecipePrefabPath)
    {
        foreach (PlatedDish platedDish in DataManager.Instance.activeDishes)
        {
            if (!platedDish.IsCompleted)
            {
                if (platedDish.DishPrefabName == associatedRecipePrefabPath && !IsIngredientSpriteActiveForDish(cardName, platedDish.PlatedDishObject))
                {
                    return platedDish.PlatedDishObject;
                }
            }

        }
        return null;
    }

    public List<PlatedDish> GetAllDishesAvailableWithAssociatedRecipe(string cardName, string associatedRecipePrefabPath)
    {
        List<PlatedDish> dishes = new List<PlatedDish>();
        foreach (PlatedDish platedDish in DataManager.Instance.activeDishes)
        {
            Debug.Log("checking singleton plateddish:" + platedDish.DishPrefabName + " for cardname: "+ cardName + "iscompleted status:"+platedDish.IsCompleted + " dishID:"+platedDish.DishID);
            if (!platedDish.IsCompleted)
            {
                Debug.Log("dish is not completed");
                if (platedDish.DishPrefabName == associatedRecipePrefabPath && !IsIngredientSpriteActiveForDish(cardName, platedDish.PlatedDishObject))
                {
                    Debug.Log(platedDish.DishPrefabName + " added to available list. dishID:"+platedDish.DishID);
                    dishes.Add(platedDish);
                }
            }

        }
        return dishes;
    }

    //scans a single prefab_BurgerPlated children objects (sprites) for the ingredient name
    private bool IsIngredientSpriteActiveForDish(string cardName, GameObject dish)
    {
        foreach (SpriteRenderer spriteRenderer in dish.GetComponentsInChildren<SpriteRenderer>())
        {
            if (spriteRenderer.gameObject.name == cardName && spriteRenderer.enabled)
            {
                Debug.Log("sprite is already enabled for" + spriteRenderer.gameObject.name);
                return true;
            }
        }
        return false;
    }

    private bool IsDishComplete(GameObject platedDish)
    {
        foreach (SpriteRenderer spriteRenderer in platedDish.GetComponentsInChildren<SpriteRenderer>())
        {
            if (!spriteRenderer.enabled)
            {
                Debug.Log("dish is incomplete because not all renderers are enabled");
                return false;
            }
        }
        return true;
    }
}
