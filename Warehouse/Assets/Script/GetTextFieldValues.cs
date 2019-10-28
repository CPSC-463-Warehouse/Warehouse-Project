using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
        Scene scene = SceneManager.GetActiveScene();

        ItemName = ItemNameIF.text;
        int.TryParse(XValueIF.text, out XValue);
        int.TryParse(YValueIF.text, out YValue);
        int.TryParse(ZValueIF.text, out ZValue);

        if (scene.name == "CreatingWarehouse")
        {
            Algo.initWarehouse(XValue, YValue, ZValue);
            Debug.Log("Warehouse");

        }
        else if (scene.name == "NewItem")
        {
            Algo.Item NewItem = new Algo.Item(ItemName, XValue, YValue, ZValue);
            Algo.addItem(NewItem);
            Debug.Log("New Item");
            //  Algo.printWarehouse();
        }
    }


}
