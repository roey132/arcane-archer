using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StatsItem : ItemData
{
    public bool IsBuff;
    public enum Stat
    {
        MovementSpeed,
        MagicDamage,
        DamagePercantage,
        NumOfArrows,
        AttackSpeed
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
                IngameStats.Instance.changeMovementSpeed(value); break;
            case Stat.AttackSpeed:
                IngameStats.Instance.changeAttackSpeed(value); break;
            case Stat.NumOfArrows:
                IngameStats.Instance.changeNumOfArrows(value); break;
            case Stat.DamagePercantage:
                IngameStats.Instance.changePhysicalMultipler(value) ; break;
        }
    }
}
public class effect
{


    //public Stat _stat;
    public float Value;
}