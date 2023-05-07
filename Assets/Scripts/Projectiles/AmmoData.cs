using UnityEngine;

[CreateAssetMenu(fileName = "New Ammo", menuName = "Ammo")]
public class AmmoData : ScriptableObject
{
    public string ammoName;
    public float speed;
    public int damage;
    public Sprite sprite;
    public string effectType;
    public string effectName;
}