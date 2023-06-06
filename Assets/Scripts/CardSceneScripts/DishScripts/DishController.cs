using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DishController : MonoBehaviour
{
    private Vector3 mousePostiionOffset;
    private float spawnedYCoordinate;
    private float spawnedXCoordinate;
    public bool isOnTrash = false;
    public UnityEvent OnDishTrashed;

    void Start()
    {
        spawnedXCoordinate = transform.position.x;
        spawnedYCoordinate = transform.position.y;
        transform.SetParent(GameObject.Find("DishSpawner").transform);
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
        Debug.Log("onmouse up, isontrash:" + isOnTrash);
        if (isOnTrash)
        {
            OnDishTrashed.Invoke();
            Destroy(gameObject);
        }
        else
        {
            //snap back to original location
            transform.position = new Vector3(spawnedXCoordinate, spawnedYCoordinate);

        }
    }
}
