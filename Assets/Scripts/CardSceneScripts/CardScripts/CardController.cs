using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardController : MonoBehaviour
{
    private Vector3 mousePostiionOffset;
    private float lastClickTime = 0f;
    private float doubleClickTimeThreshold = 0.3f;

    private float spawnedYCoordinate;
    private float spawnedXCoordinate;

    public string associatedRecipePrefabPath;
    public string cardName;

    void Start()
    {
        spawnedXCoordinate = transform.localPosition.x;
        spawnedYCoordinate = transform.localPosition.y;
    }

    public void setCardMetaData(Card card)
    {
        this.cardName = card.CardName;
        this.associatedRecipePrefabPath = card.AssociatedRecipePrefabPath;
        Debug.Log("cardcontroller cardname:" +cardName);
    }

    private void OnMouseUpAsButton()
    {
        if (Time.time - lastClickTime < doubleClickTimeThreshold)
        {
            // Perform action on double-click
            Debug.Log("Sprite double-clicked!");
            //attempt to use card toward an active order
            //if successful, consume card, perform actions
            //gameObject.GetComponent ...
            //Destroy(gameObject);
        }
        lastClickTime = Time.time;
    }

    private Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        mousePostiionOffset = gameObject.transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + mousePostiionOffset;
    }

    private void OnMouseUp()
    {
        if(transform.localPosition.y > -0.5)
        {
            var dishSpawner = GameObject.Find("DishSpawner").GetComponent<DishSpawner>();
            var emptyDish = dishSpawner.InstantiateDish();
            
            var newDish = dishSpawner.InstantiatePlatedDish(associatedRecipePrefabPath, emptyDish);
            var platedDishController = newDish.GetComponentInChildren<PlatedDishController>();

            platedDishController.EnableDishIngredientSpriteByCardName(cardName);

            Destroy(gameObject);
        }
        else
        {
            transform.localPosition = new Vector3(spawnedXCoordinate, spawnedYCoordinate);
        }
    }
}
