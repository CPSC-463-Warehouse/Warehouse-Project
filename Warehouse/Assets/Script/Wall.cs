using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wall : MonoBehaviour
{
    public GameObject block;
    //public int width = 50;
    //public int legnth = 50;

    void Start()
    {
        int length = WarehouseInstance.instance.length;
        int width = WarehouseInstance.instance.width;

        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < length; ++y)
            {
                Instantiate(block, new Vector3(x, 0, y), Quaternion.identity);
            }
        }
    }
}