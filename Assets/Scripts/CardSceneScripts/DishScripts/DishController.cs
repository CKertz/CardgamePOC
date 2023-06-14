using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DishController : MonoBehaviour
{
    private Vector3 mousePostiionOffset;
    private float spawnedYCoordinate;
    private float spawnedXCoordinate;
    public UnityEvent OnDishTrashed;
    //isOnTrash is modified in TrashCanController to help trigger trash event
    public bool isOnTrash = false;

    private float dishServeWindowXPosition = 0.25f;
    private float dishServeWindowYPosition = 0.55f;
    private float dishServeWindowSpacing = 0.35f;


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
            DataManager.Instance.spawnedDishCount--;
            Debug.Log("spawnedDishCount updated to:" + DataManager.Instance.spawnedDishCount);

            Destroy(gameObject);
        }
        else if(IsDishOnServingWindow())
        {
            if (DataManager.Instance.dishInWindowCount < 5)
            {
                var updatedPosition = CalculateDishServeWindowPosition();
                transform.localPosition = updatedPosition;
                Debug.Log("transform name:" + transform.gameObject.name + " updated position:"+updatedPosition);
                DataManager.Instance.spawnedDishCount--;
                DataManager.Instance.dishInWindowCount++;
                Debug.Log("spawnedDishCount updated to:" + DataManager.Instance.spawnedDishCount + " , dishInWindowCount = "+DataManager.Instance.dishInWindowCount);

                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }

        }
        else
        {
            //snap back to original location
            transform.position = new Vector3(spawnedXCoordinate, spawnedYCoordinate);

        }

    }

    private bool IsDishOnServingWindow()
    {
        Debug.Log("dish transform.localposition onmouseup X:" + transform.localPosition.x + " Y:" + transform.localPosition.y);
        if (transform.localPosition.x <= 2.1f && transform.localPosition.x >= 0 && transform.localPosition.y > 0.5f)
        {
            return true;
        }
        return false;
    }

    private Vector3 CalculateDishServeWindowPosition()
    {
        float xPos = dishServeWindowXPosition + (DataManager.Instance.dishInWindowCount * dishServeWindowSpacing);
        Debug.Log("calculated window X:" + xPos);
        return new Vector3(xPos, dishServeWindowYPosition, 0);
    }

}
