using Assets.Models;
using Assets.Scripts.CardSceneScripts;
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

    //UnityEvent onCardPlayed;
    CardPlayEvent onCardPlayed;

    void Start()
    {
        PlayCardHandler playCardHandler = new PlayCardHandler();

        spawnedXCoordinate = transform.localPosition.x;
        spawnedYCoordinate = transform.localPosition.y;

        if (onCardPlayed == null)
        {
            onCardPlayed = new CardPlayEvent();
        }
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

            var dishIsSpawned = dishSpawner.AttemptToInstantiateDish(cardName, associatedRecipePrefabPath, gameObject);

            // destroying card won't always happen. i.e. in cases where card can't be played due to lack of counter space
            if (dishIsSpawned)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.localPosition = new Vector3(spawnedXCoordinate, spawnedYCoordinate);
            }
        }
        else
        {
            transform.localPosition = new Vector3(spawnedXCoordinate, spawnedYCoordinate);
        }
    }
}

[System.Serializable]
public class CardPlayEvent : UnityEvent<string,string,GameObject>
{

}

/*
 * in datamanger -> list<plateddish> activedishes
 * PlatedDish model:
 *      +DishName -> make a mapper to make it more readable? i.e. a dict for  DishName = burger, AssociatedRecipe = prefab_BurgerPlated
 *      +IsCompleted
 * 
 * algorithm:
 * 
 * a card is played. 
 * 
 * if it's an ingredient
 *     if 5 dishes are currently active AND (no active dishes have card's associatedrecipe OR the ones that do already have this ingredient on them)
 *          too many dishes, disallow
 *          
 *     if no activedish exists with the same associatedrecipe, 
 *          create a new dish and enable sprite of cardname
 *          scan all spriterenderers on plate and see if dish is done (1 ingredient dishes edge case)
 *          if all spriterenderers in plateddish are enabled, 
 *              order is complete, mark as no longer active/complete
 *          else enable the sprite of cardname
 *     else
 *          get activedish with same associated recipe AND !IsCompleted
 *          check if spriterenderer can be enabled on it.
 *              if it is already enabled, 
 *                  if less than 5 dishes active
 *                      make a new plate with new ingredient 
 *                  else disallow
 *              else enable spriterenderer
 *          
 *  
 */