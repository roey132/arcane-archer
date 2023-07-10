using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newStatItem",menuName = "Items/StatItem")]
public class StatsItem : ItemData
{
    public bool IsBuff;
    public enum Stat
    {
        MovementSpeed,
        Damage,
        DamagePercantage,
        MagicDamage,
        MagicDamagePercantage,
        NumOfArrows,
        AttackSpeed,
        MaxHp,
        CriticalRate,
        CriticalDamage,
        CurrencyGain,
        FireDamage,
        IceDamage

    }

    public Stat[] Stats;
    public float[] Values;

    public override void ApplyItem()
    {
        for (int i = 0; i < Stats.Length; i++)
        {
            handleStat(Stats[i], Values[i]);
        }
    }
    public void handleStat(Stat stat, float value)
    {
        switch (stat) 
        {
            case Stat.MovementSpeed:
                IngameStats.Instance.ChangeMovementSpeed(value); break;
            case Stat.AttackSpeed:
                IngameStats.Instance.ChangeAttackSpeed(value); break;
            case Stat.NumOfArrows:
                IngameStats.Instance.ChangeNumOfArrows(value); break;
            case Stat.Damage:
                break;
            case Stat.DamagePercantage:
                IngameStats.Instance.ChangePhysicalMultipler(value) ; break;
            case Stat.MagicDamage:
                break;
            case Stat.MagicDamagePercantage:
                break;
            case Stat.MaxHp:
                break;
            case Stat.CriticalRate:
                break;
            case Stat.CriticalDamage:
                break;
            case Stat.CurrencyGain:
                break;
            case Stat.FireDamage:
                break;
            case Stat.IceDamage:
                break;

        }
    }
}
public class effect
{


    //public Stat _stat;
    public float Value;
}