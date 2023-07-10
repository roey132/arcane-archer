using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseData : MonoBehaviour
{
    public static PlayerBaseData Instance;

    [Header("BaseStats")]
    public float Health;
    public float Mana;
    public float MovementSpeed;
    public float AttackSpeed;
    public float CastSpeed;
    public float CriticalRate;
    public float BaseDamage;
    public float BaseMagicDamage;
    public int NumOfArrows;

    [Header("Multipliers")]
    public float CriticalDamageMultiplier;
    public float PhysicalDamageMultiplier;
    public float MagicalDamageMultiplier;

    [Header("Elemental Stats")]
    public float FireDamageMultiplier;
    public float IceDamageMultiplier;

    [Header("CurrencyRelated")]
    public int StartingIngameCurrency;
    public int CurrencyMultiplier;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}

