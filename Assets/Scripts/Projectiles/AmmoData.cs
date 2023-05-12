using UnityEngine;

[CreateAssetMenu(fileName = "New Ammo", menuName = "Ammo")]
public class AmmoData : ScriptableObject
{
    public string ammoName;
    public float speed;
    public float damage;
    public Sprite sprite;
    public string effectType;
    public string effectName;
}