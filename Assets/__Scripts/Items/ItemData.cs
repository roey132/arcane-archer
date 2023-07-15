using UnityEngine;

public class ItemData : ScriptableObject, WeightedItem
{
    public enum ItemType
    {
        Archery,
        Magical,
        Hybrid,
        Utility
    }
    public enum ItemRarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }
    public enum ItemElement
    {
        Neutral,
        Fire,
        Ice
    }

    public string ItemName;
    public Sprite ItemIcon;
    public ItemType Type;
    public ItemRarity Rarity;
    public string ItemDescription;
    public ItemElement Element;
    public float ItemBasePrice;
    public int BaseWeight;

    // TODO change weight to work with item rarity, item element and type, scaling with player choices
    // TODO Change base price to scale with the game and rarity

    public virtual void ApplyItem()
    {

    }
    public int Weight()
    {
        return BaseWeight;
    }
}
