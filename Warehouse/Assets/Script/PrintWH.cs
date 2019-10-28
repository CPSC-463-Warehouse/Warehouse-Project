using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintWH : MonoBehaviour
{
    Text instruction;
    // Start is called before the first frame update
    void Start()
    {
        instruction = GetComponent<Text>();
        instruction.text = Algo.printWarehouse();
    }

    // Update is called once per frame
    void Update()
    {
        instruction = GetComponent<Text>();
        instruction.text = Algo.printWarehouse();
    }
}
