using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBaseData : MonoBehaviour
{
    public static playerBaseData Instance;

    public float Health;
    public float Mana;
    public float MovementSpeed;
    public float AttackSpeed;
    public float CastSpeed;
    public float CriticalRate;
    public float CriticalDamage;
    public float BaseDamage;
    public float BaseMagicDamage;
    public float PhysicalDamageMultiplier;
    public float MagicalDamageMultiplier;
    public int NumOfArrows;
    public int StartingIngameCurrency;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}


