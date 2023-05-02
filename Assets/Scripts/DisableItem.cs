using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableItem : MonoBehaviour
{
    private GameObject[] wholeItemSystems;
    private GameObject wholeItemSystem;
    private GameObject itemParent;

    void Start()
    {
        wholeItemSystems = GameObject.FindGameObjectsWithTag("WholeItemSystem");
        foreach (GameObject wholeItemSystem in wholeItemSystems)
        {
            itemParent = wholeItemSystem.transform.Find("ItemParent").gameObject;   
            if (itemParent != null)
            {   
                itemParent.SetActive(false);
            }
        }
    }
}
