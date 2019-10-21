using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToCreatingWarehouseScene()
    {
        SceneManager.LoadScene("CreatingWarehouse");
    }

    public void GoToMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void GoToListViewScene()
    {
        SceneManager.LoadScene("List View");
    }

    public void GoToViewWarehouseScene()
    {
        SceneManager.LoadScene("ViewWarehouse");
    }

    public void GoToNewItem()
    {
        SceneManager.LoadScene("NewItem");
    }
}
