using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanController : MonoBehaviour
{
    private bool isDragging = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Dish")
        {
            isDragging = true;
            col.gameObject.GetComponent<DishController>().isOnTrash = true;
        }
    }

    private void OnMouseUp()
    {
        if (isDragging && Input.GetMouseButtonUp(0))
        {
            Debug.Log("mouse button up");
        }
    }
}
