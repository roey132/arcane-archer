using UnityEngine;

public class ItemData : ScriptableObject, WeightedItem
{
    public string ItemName;
    public Sprite ItemIcon;
    public string ItemType;
    public string ItemDescription;
    public string ItemElement;
    public float ItemBasePrice;
    public int BaseWeight;

    public virtual void ApplyItem()
    {

    }
    public int Weight()
    {
        return BaseWeight;
    }
}
