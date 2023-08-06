using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Arrow", menuName = "Arrow")]
public class ArrowData : ScriptableObject
{
    public string ArrowName;
    public float Speed;
    public float Damage;
    public Sprite Sprite;
    public string EffectType;
    public string EffectName;
    public GameObject Effect;
    public DebuffData Debuff;
    public int PenetrateCount;
    public ArrowBehaviour arrow;
    public GameObject ArrowPrefab;
    public virtual GameObject ActivateEffect(Vector2 position)
    {
        GameObject effectObject = Instantiate(Effect ,position, Quaternion.identity);
        return effectObject;
    }
}