using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "newArrowItem",menuName = "Items/ArrowItem")]
public class ArrowItem : ItemData
{
    public ArrowData ArrowData;
    public int ArrowCount;

    public override void ApplyItem()
    {
        ArrowInventory.Instance.AddArrow(ArrowData,ArrowCount);
    }
}
