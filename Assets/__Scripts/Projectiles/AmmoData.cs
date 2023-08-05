using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ammo", menuName = "Ammo")]
public class AmmoData : ScriptableObject
{
    public string AmmoName;
    public float Speed;
    public float Damage;
    public Sprite Sprite;
    public string EffectType;
    public string EffectName;
    public GameObject Effect;
    public DebuffData Debuff;
    public int PenetrateCount;
    public virtual GameObject ActivateEffect(Vector2 position)
    {
        GameObject effectObject = Instantiate(Effect ,position, Quaternion.identity);
        return effectObject;
    }
}