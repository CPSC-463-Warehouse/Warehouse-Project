using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseInstance : MonoBehaviour
{
    public static WarehouseInstance instance; //the singleston

    public int length;
    public int width;
    public int height;

    public bool[,,] warehouse;
    public string[] itemnames;
    public int itemindex = 0;

    public bool isValid = false;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void setWarehouse()
    {
        warehouse = new bool[length, width, height];
        isValid = true;
    }
}
