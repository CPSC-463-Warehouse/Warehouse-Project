using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveWarehouseInfo : MonoBehaviour
{

    public InputField inputText;
    string sText;

    void Start()
    {
        sText = PlayerPrefs.GetString("sTextName");
        inputText.text = sText;
    }


    public void SaveThis()
    {
        sText = inputText.text;
        PlayerPrefs.SetString("sTextName", sText);
    }

}
