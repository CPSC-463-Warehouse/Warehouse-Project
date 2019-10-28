using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetTextFieldValues : MonoBehaviour
{
    public Button EnterButton;
    public InputField ItemNameIF, XValueIF, YValueIF, ZValueIF;
    string ItemName;
    int XValue, YValue, ZValue;

    // Start is called before the first frame update
    void Start()
    {
        XValue = 0;
        YValue = 0;
        ZValue = 0;
        ItemName = "None";

        EnterButton.onClick.AddListener(ProcessText);

    }

    void ProcessText()
    {
        ItemName = ItemNameIF.text;
        int.TryParse(XValueIF.text, out XValue);
        int.TryParse(YValueIF.text, out YValue);
        int.TryParse(ZValueIF.text, out ZValue);

        Debug.Log(ItemName + " " + XValue + " " + YValue + " " + ZValue);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
