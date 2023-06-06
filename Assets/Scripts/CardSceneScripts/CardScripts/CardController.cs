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
    public UnityEvent OnCardPlayed;

    public string associatedRecipePrefabPath;

    void Start()
    {
        spawnedXCoordinate = transform.localPosition.x;
        spawnedYCoordinate = transform.localPosition.y;
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
            dishSpawner.InstantiatePlatedDish(associatedRecipePrefabPath);
            OnCardPlayed.Invoke();
            Destroy(gameObject);
        }
        else
        {
            transform.localPosition = new Vector3(spawnedXCoordinate, spawnedYCoordinate);
        }
    }
}
