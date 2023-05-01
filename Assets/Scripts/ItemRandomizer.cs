using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite itemImage;
    public int itemAttackPoints;
    public int itemLifePoints;
    public int itemProficiencyPoints;
    public int itemExcelencePoints;

    public Item(string name, Sprite image, int attackPoints, int lifePoints, int proficiencyPoints, int excelencePoints)
    {
        this.itemName = name;
        this.itemImage = image;
        this.itemAttackPoints = attackPoints;
        this.itemLifePoints = lifePoints;
        this.itemProficiencyPoints = proficiencyPoints;
        this.itemExcelencePoints = excelencePoints;
    }
}

public class ItemRandomizer : MonoBehaviour
{
    public Sprite[] itemSprites;
    private static List<Item> availableItems = new List<Item>();

    public void LoadItemList()
    {
        availableItems = new List<Item>()
        {
            new Item("EntropyBoots", itemSprites[0], 10, 20, 0, 0),
            new Item("NightmareMonolyth", itemSprites[1], 0, -40, 5, 2),
            new Item("BoneOfTheDead", itemSprites[2],50, -80, 0, 0),
            new Item("HollowLight", itemSprites[3], -10, 50, 2, 0),
            new Item("SoulessGem", itemSprites[4], 0, -100, 10, 5),
            new Item("DemonHands", itemSprites[5], 20, 50, -1, 0),
            new Item("VoidHelmet", itemSprites[6], 100, 0, 15, 10),
            new Item("HeavensHelmet", itemSprites[7], 0, 0, 0, 0), 
            new Item("OrtraxAmulet", itemSprites[8], 20, 50, 5, 3),
            new Item("LifeAmulet", itemSprites[9], -15, 80, 0, 0),
            new Item("OrtraxRing", itemSprites[10], 10, 10, 0, 0),
            new Item("CursedScroll", itemSprites[11], -30, -50, 10, 5),
            new Item("AncientSkull", itemSprites[12], 10, 50, -2, 0),
            new Item("BloodthirstySword", itemSprites[13], 50, -50, 10, 0),
            new Item("AngelsChestpiece", itemSprites[14], -10, 100, 0, 4)
        };
    }

    public Item GetRandomItem()
    {
        // escolhe um item aleatório da lista de itens disponíveis
        int index = Random.Range(0, availableItems.Count);
        Item item = availableItems[index];

        // remove o item escolhido da lista de itens disponíveis
        availableItems.RemoveAt(index);

        return item;
    }
}
