using UnityEngine;

public class ItemData : ScriptableObject
{
    public string ItemName;
    public Sprite ItemIcon;
    public string ItemType;
    public string ItemDescription;
    public string ItemElement;

    public virtual void ApplyItem()
    {

    }
}
