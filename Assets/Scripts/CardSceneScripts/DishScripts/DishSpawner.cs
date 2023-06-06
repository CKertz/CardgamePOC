using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishSpawner : MonoBehaviour
{
    public GameObject dishPrefab;
    private float dishXPosition = -1;
    private float dishYPosition = -0.2f;
    private float dishSpacing = 0.5f;

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

    public void InstantiatePlatedDish(string associatedRecipe, GameObject emptyDish)
    {
        GameObject associatedRecipePrefab = Resources.Load<GameObject>(associatedRecipe);
        if (associatedRecipePrefab == null )
        {
            Debug.Log("AssociatedRecipe prefab not found: " + associatedRecipe);
            return;
        }
        GameObject spawnedPrefab = Instantiate(associatedRecipePrefab);
        spawnedPrefab.transform.SetParent(emptyDish.transform, false);
    }

    private Vector3 CalculateDishSpawnPosition()
    {
        float xPos = dishXPosition + (DataManager.Instance.spawnedDishCount * dishSpacing);
        return new Vector3(xPos, dishYPosition, 0);
    }
}
