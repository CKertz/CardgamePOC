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

    public void AttemptToInstantiateDish(string cardName, string associatedRecipePrefabPath, GameObject cardObject)
    {
        Debug.Log("AttemptToInstantiateDish called. cardname" + cardName);
        var availableDishList = GetAllDishesAvailableWithAssociatedRecipe(cardName, associatedRecipePrefabPath);
        //var dishToEdit = GetFirstDishAvailableWithAssociatedRecipe(cardName, associatedRecipePrefabPath);
        if (DataManager.Instance.activeDishes.Count >= 5 && availableDishList.Count == 0 )
        {
            Debug.LogWarning("There is no room for this card to be played");
            return;
        }

        if (availableDishList.Count == 0 && DataManager.Instance.activeDishes.Count < 5)
        {
            Debug.Log("availabledishlist is 0 and activedishes < 5");
            //there is room to make a new dish and we are making a fresh one, not contributing to an existing one
            var emptyDish = InstantiateDish();

            var newDish = InstantiateFoodPrefab(associatedRecipePrefabPath, emptyDish);
            var platedDishController = newDish.GetComponentInChildren<PlatedDishController>();
            platedDishController.EnableDishIngredientSpriteByCardName(cardName);
            // check if dish is done (1 card orders)
            if(IsDishComplete(newDish))
            {
                DataManager.Instance.activeDishes.Add(new PlatedDish(associatedRecipePrefabPath, isCompleted: true, newDish));
            }
            else
            {
                DataManager.Instance.activeDishes.Add(new PlatedDish(associatedRecipePrefabPath, isCompleted: false, newDish));
            }

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
                    Debug.Log("sprite is able to activate for dish");
                    var platedDishController = dish.PlatedDishObject.GetComponentInChildren<PlatedDishController>();
                    platedDishController.EnableDishIngredientSpriteByCardName(cardName);

                    if (IsDishComplete(dish.PlatedDishObject))
                    {
                        //TODO: don't think this is right
                        DataManager.Instance.activeDishes.Add(new PlatedDish(associatedRecipePrefabPath, isCompleted: true, dish.PlatedDishObject));
                    }
                    break;
                }
            }
        }

    }

    private GameObject InstantiateDish()
    {
        //handle transform positioning (lines of multiple dishes)
        //handle passing on correct food prefab to dishcontroller
        var adjustedSpawnPosition = CalculateDishSpawnPosition();
        GameObject dish = Instantiate(dishPrefab, adjustedSpawnPosition, Quaternion.identity);
        //dish.transform.parent = transform.parent;
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
            Debug.Log("checking singleton plateddish:" + platedDish.DishPrefabName + " for cardname: "+ cardName + "iscompleted status:"+platedDish.IsCompleted);
            if (!platedDish.IsCompleted)
            {
                Debug.Log("dish is not completed");
                if (platedDish.DishPrefabName == associatedRecipePrefabPath && !IsIngredientSpriteActiveForDish(cardName, platedDish.PlatedDishObject))
                {
                    Debug.Log(platedDish.DishPrefabName + " added to available list");
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
        //gameObject instead of platedDish?
        foreach (SpriteRenderer spriteRenderer in platedDish.GetComponentsInChildren<SpriteRenderer>())
        {
            if (!spriteRenderer.enabled)
            {
                Debug.Log("sprite is disabled for" + spriteRenderer.gameObject.name);
                return false;
            }
        }
        return true;
    }
}
