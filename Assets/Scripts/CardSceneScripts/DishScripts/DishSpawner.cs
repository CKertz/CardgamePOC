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
        var dishToEdit = GetFirstDishAvailableWithAssociatedRecipe(cardName, associatedRecipePrefabPath);
        if (DataManager.Instance.activeDishes.Count >= 5 && dishToEdit == null)
        {
            Debug.LogWarning("There is no room for this card to be played");
            return;
        }

        if (dishToEdit == null)
        {
            //there is room to make a new dish and we are making a fresh one, not contributing to an existing one
            var emptyDish = InstantiateDish();

            var newDish = InstantiateFoodPrefab(associatedRecipePrefabPath, emptyDish);
            var platedDishController = newDish.GetComponentInChildren<PlatedDishController>();
            platedDishController.EnableDishIngredientSpriteByCardName(cardName);

            DataManager.Instance.activeDishes.Add(new PlatedDish(associatedRecipePrefabPath, false, newDish));

        }
        else
        {

        }

    }

    public GameObject InstantiateDish()
    {
        //handle transform positioning (lines of multiple dishes)
        //handle passing on correct food prefab to dishcontroller
        var adjustedSpawnPosition = CalculateDishSpawnPosition();
        GameObject dish = Instantiate(dishPrefab, adjustedSpawnPosition, Quaternion.identity);
        //dish.transform.parent = transform.parent;
        DataManager.Instance.spawnedDishCount++;
        return dish;
    }

    public GameObject InstantiateFoodPrefab(string associatedRecipe, GameObject emptyDish)
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
                if (platedDish.DishPrefabName == associatedRecipePrefabPath && !IsIngredientSpriteActiveForDish(cardName))
                {
                    return platedDish.PlatedDishObject;
                }
            }

        }
        return null;
    }

    //scans a single prefab_BurgerPlated children objects (sprites) for the ingredient name
    private bool IsIngredientSpriteActiveForDish(string cardName)
    {
        foreach (SpriteRenderer spriteRenderer in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            if (spriteRenderer.gameObject.name == cardName && spriteRenderer.enabled)
            {
                Debug.Log("sprite is already enabled for" + spriteRenderer.gameObject.name);
                return true;
            }
        }
        return false;
    }
}
