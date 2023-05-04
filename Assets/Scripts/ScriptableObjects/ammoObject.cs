using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ammo", menuName = "Ammo")]
public class AmmoObject : ScriptableObject
{
    public string AmmoName;
    public float Speed;
    public float Damage;
    public Sprite Sprite;

}
