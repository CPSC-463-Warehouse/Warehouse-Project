using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatingWarehouse : MonoBehaviour
{
    //change these to warehouse size variables x & z = size of square y = height of sqare
    public float x = 30f;
    public float y = 30f;
    public float z = 25f;

    // create bottom plane to new warehouse size 
    void Start()
    {
        transform.localScale = new Vector3(x, y, z);
    }
}
