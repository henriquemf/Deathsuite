using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static List<Item> inventoryItems = new List<Item>();
    
    private void Awake()
    {
        DontDestroyOnLoad(this.transform.root.gameObject);
    }
}
