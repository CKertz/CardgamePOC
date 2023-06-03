using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketSpikeController : MonoBehaviour
{
    private bool isDragging = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("OnCollisionEnter2D");
        if(col.gameObject.name == "prefab_CustomerOrder")
        {
            isDragging = true;
            Debug.Log("isdragging:" + isDragging);
            col.gameObject.GetComponent<OrderController>().isOnSpike = true;
            Debug.Log("collision- isonspike=" + col.gameObject.GetComponent<OrderController>().isOnSpike);
        }
    }

    private void OnMouseUp()
    {
        if(isDragging && Input.GetMouseButtonUp(0))
        {
            Debug.Log("mouse button up");
        }
    }

}
