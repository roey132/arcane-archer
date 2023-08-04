using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStatItem", menuName = "Items/SpellItem")]

public class SpellItem : ItemData
{
    public GameObject SpellObject;
    public override void ApplyItem()
    {
        SpellManager.Instance.AddSpell(SpellObject);
    }
}
