using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstTimeLoader : MonoBehaviour
{
    public static FirstTimeLoader instance;
    public static bool firstTimeLoading;
    private GameObject UI;
    private GameObject UI_child1;
    private GameObject UI_child2;
    private GameObject UI_child3;
    private GameObject UI_child4;
    private GameObject UI_child5;
    private GameObject UI_child6;

    void Awake()
    {
        UI = GameObject.Find("UI");
        UI_child1 = UI.transform.GetChild(0).gameObject;
        UI_child2 = UI.transform.GetChild(1).gameObject;
        UI_child3 = UI.transform.GetChild(2).gameObject;
        UI_child4 = UI.transform.GetChild(3).gameObject;
        UI_child5 = UI.transform.GetChild(4).gameObject;
        UI_child6 = UI.transform.GetChild(5).gameObject;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            firstTimeLoading = true;
            InventoryManager.inventoryItems.Clear();
            ResetInventoryImages();
        }
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void ResetInventoryImages()
    {
        UI_child1.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        UI_child2.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        UI_child3.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        UI_child4.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        UI_child5.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        UI_child6.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
    }
}
