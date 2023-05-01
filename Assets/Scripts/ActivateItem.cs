using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateItem : MonoBehaviour
{
    private GameObject itemParent;

    void Start()
    {
        itemParent = GameObject.Find("ItemParent");   
        itemParent.SetActive(false);
    }
    void Update()
    {
        if (EnemySpawner.mobCnt <= 0)
        {
            itemParent.SetActive(true);
        }
    }
}
