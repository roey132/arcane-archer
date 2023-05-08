using UnityEngine;

public class SpellData : ScriptableObject
{
    public float Cooldown;
    public float SpellDamage;
    public float SpellType;
    public float SpellName;
    public float SpellElement;

    public virtual void Activate()
    {

    }
}
