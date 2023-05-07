using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    private Vector3 mousePostiionOffset;
    private float lastClickTime = 0f;
    private float doubleClickTimeThreshold = 0.3f;

    private float spawnedYCoordinate;
    private float spawnedXCoordinate;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.name);
        spawnedXCoordinate = transform.localPosition.x;
        spawnedYCoordinate = transform.localPosition.y;

        Debug.Log("x:" + spawnedXCoordinate + " y:" + spawnedYCoordinate);

    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (transform.localPosition.y > -0.3)
        {
            Debug.Log("in consume zone");
        }
    }

    private void OnMouseUp()
    {
        //if y position is > -0.2, consume card else snap back to original location
        if(transform.localPosition.y > -0.4)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.localPosition = new Vector3(spawnedXCoordinate, spawnedYCoordinate);
        }
    }
}
