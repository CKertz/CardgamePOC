using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(GameObject.Find("DishSpawner").transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
