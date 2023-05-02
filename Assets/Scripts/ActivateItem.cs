using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateItem : MonoBehaviour
{
    private GameObject[] wholeItemSystems;
    private GameObject wholeItemSystem;
    private GameObject itemParent;

    void Awake()
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
    void Update()
    {
        Debug.Log("Mob Count: " + EnemySpawner.mobCnt);
        if (EnemySpawner.mobCnt <= 0)
        {
            itemParent.SetActive(true);
        }
        else
        {
            itemParent.SetActive(false);
        }
    }
}
