using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int maxCapacity = 6;

    public bool AddItem(Item item)
    {
        //Debug.Log("Adicionando item " + item.itemName + " ao inventário.");
        //Debug.Log("Inventário possui " + InventoryManager.inventoryItems.Count + " itens.");
        // Verifica se o inventário está cheio
        if (InventoryManager.inventoryItems.Count >= maxCapacity)
        {
            //Debug.Log("Inventário cheio!");
            return false;
        }

        // Adiciona o item ao inventário
        if (InventoryManager.inventoryItems.Contains(item))
        {
            //Debug.Log("Item já está no inventário!");
            return false;
        }
        else
        {
            InventoryManager.inventoryItems.Add(item);
            //Debug.Log(item.itemName + " adicionado ao inventário!");
            return true;
        }
    }

    public List<Item> GetItems()
    {
        return InventoryManager.inventoryItems;
    }
}
