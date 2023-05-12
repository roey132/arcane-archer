using UnityEngine;

public class SpellData : ScriptableObject
{
    public float Cooldown;
    public int SpellDamage;
    public string SpellType;
    public string SpellName;
    public string SpellElement;
    public bool isActiveSpell;
    public int _effectDurationMilliSeconds;

    public virtual GameObject Activate(Vector2 center, Transform playerTransform)
    {
        GameObject temp = new GameObject();
        return temp;
    }
    public virtual void DeleteSpell(GameObject spellObject) 
    {

    }
}
