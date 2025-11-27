using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemSO", menuName = "Custom Assets/Shop Item")]
public class ItemSO : ScriptableObject
{
    public string ItemName;
    public int ItemCost;
    public Sprite ItemIcon;
    public ItemRarity Rarity;

    public void BuyItem()
    {
        //  Validamos currency y consumimos.
        Debug.Log($"Se compro el item {ItemName}");
        GiveItem();
    }

    private void GiveItem() { }
}

public enum ItemRarity { Common, Rare, UltraRare, Legendary }
