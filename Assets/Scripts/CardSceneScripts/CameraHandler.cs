using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Camera CardCamera;
    public Camera CustomerCamera;

    void Start()
    {
        CardCamera.enabled = true;
        CustomerCamera.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CardCamera.enabled = !CardCamera.enabled;
            CustomerCamera.enabled = !CustomerCamera.enabled;
        }
    }
}
