using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsText : MonoBehaviour
{
    public TextMeshPro attackText;
    public TextMeshPro lifeText;
    public TextMeshPro proText;
    public TextMeshPro exlText;

    void Start()
    {
        attackText.text = " ";
        lifeText.text = " ";
        proText.text= " ";
        exlText.text= " ";
    }


    public void GenerateText(Item item)
    {
        ItemController.itemGameObject = item;

        if (item.itemAttackPoints < 0) {
            attackText.color = Color.red;
        } else {
            attackText.color = Color.green;
        }

        if (item.itemLifePoints < 0) {
            lifeText.color = Color.red;
        } else {
            lifeText.color = Color.green;
        }

        if (item.itemProficiencyPoints < 0) {
            proText.color = Color.red;
        } else {
            proText.color = Color.green;
        }

        if (item.itemExcelencePoints < 0) {
            exlText.color = Color.red;
        } else {
            exlText.color = Color.green;
        }

        if (item.itemName == "VoidHelmet") 
        {
            attackText.text = item.itemAttackPoints.ToString();
            lifeText.color = new Color(0.0f, 0.0f, 1.0f);
            lifeText.text = "1 HP";
            proText.text = item.itemProficiencyPoints.ToString();
            exlText.text = item.itemExcelencePoints.ToString();
        }

        else if (item.itemName == "HeavensHelmet")
        {
            attackText.color = new Color(0.0f, 0.0f, 1.0f);
            attackText.text = "-20%";
            lifeText.color = new Color(0.0f, 0.0f, 1.0f);
            lifeText.text = "x2";
            proText.text = item.itemProficiencyPoints.ToString();
            exlText.text = item.itemExcelencePoints.ToString();
        }

        else if (item.itemName == "OrtraxRing") 
        {
            foreach (Item iteminlist in InventoryManager.inventoryItems)
            {
                if (iteminlist.itemName == "OrtraxAmulet")
                {
                    attackText.color = new Color(0.0f, 0.0f, 1.0f);
                    attackText.text = "10x2";
                    lifeText.color = new Color(0.0f, 0.0f, 1.0f);
                    lifeText.text = "10x2";
                    proText.text = item.itemProficiencyPoints.ToString();
                    exlText.text = item.itemExcelencePoints.ToString();
                    break;
                }
                else
                {
                    attackText.color = new Color(0.0f, 0.0f, 1.0f);
                    attackText.text = "10";
                    lifeText.color = new Color(0.0f, 0.0f, 1.0f);
                    lifeText.text = "10";
                    proText.text = item.itemProficiencyPoints.ToString();
                    exlText.text = item.itemExcelencePoints.ToString();
                    break;
                }
            }

            if (InventoryManager.inventoryItems.Count == 0)
            {
                attackText.color = new Color(0.0f, 0.0f, 1.0f);
                attackText.text = "10";
                lifeText.color = new Color(0.0f, 0.0f, 1.0f);
                lifeText.text = "10";
                proText.text = item.itemProficiencyPoints.ToString();
                exlText.text = item.itemExcelencePoints.ToString();
            }
        }

        else {
            attackText.text = item.itemAttackPoints.ToString();
            lifeText.text = item.itemLifePoints.ToString();
            proText.text = item.itemProficiencyPoints.ToString();
            exlText.text = item.itemExcelencePoints.ToString();
        }
    }

    public void ClearText()
    {
        attackText.text = " ";
        lifeText.text = " ";
        proText.text= " ";
        exlText.text= " ";
    }
}
