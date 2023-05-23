using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "newAmmoItem",menuName = "Items/AmmoItem")]
public class AmmoItem : ItemData
{
    public AmmoData AmmoData;
    public int AmmoCount;

    public override void ApplyItem()
    {
        AmmoInventory.Instance.addAmmo(AmmoData,AmmoCount);
    }
}
