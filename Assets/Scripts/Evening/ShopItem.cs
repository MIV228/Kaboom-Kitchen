using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//[CreateAssetMenu(fileName = "Item", menuName = "Shop Item", order = 1)]
public class ShopItem : MonoBehaviour
{
    public int ID;

    public int cost;
    public GameObject item;

    public int min_door = 0;

    public enum ItemType
    {
        Consumable,
        Reroll
    }

    public ItemType type;

    public enum Rarity
    {
        Normal,
        Uncommon,
        Rare
    }

    public Rarity rarity;
}