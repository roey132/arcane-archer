using UnityEngine;



[CreateAssetMenu(fileName = "NewStatItem",menuName = "Items/StatItem")]
public class StatsItem : ItemData
{
    public bool IsBuff;
    public enum Stat
    {
        MaxHp,
        MovementSpeed,
        Damage,
        DamageMultiplier,
        MagicDamage,
        MagicDamageMultiplier,
        NumOfArrows,
        AttackSpeed,
        CriticalRate,
        CriticalDamageMultiplier,
        CurrencyGainMultiplier,
        FireDamageMultiplier,
        IceDamageMultiplier
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
                IngameStats.Instance.ChangeDamage(value); break;

            case Stat.DamageMultiplier:
                IngameStats.Instance.ChangeDamageMultipler(value) ; break;

            case Stat.MagicDamage:
                IngameStats.Instance.ChangeMagicalDamage(value); break;

            case Stat.MagicDamageMultiplier:
                IngameStats.Instance.ChangeMagicalDamageMultiplier(value); break;

            case Stat.MaxHp:
                IngameStats.Instance.ChangeMaxHp(value); break;

            case Stat.CriticalRate:
                IngameStats.Instance.ChangeCriticalRate(value); break;

            case Stat.CriticalDamageMultiplier:
                IngameStats.Instance.ChangeCriticalDamageMultiplier(value); break;

            case Stat.CurrencyGainMultiplier:
                IngameStats.Instance.ChangeCurrencyGainMultiplier(value); break;

            case Stat.FireDamageMultiplier:
                IngameStats.Instance.ChangeFireDamageMultiplier(value); break;

            case Stat.IceDamageMultiplier:
                IngameStats.Instance.ChangeIceDamageMultiplier(value); break;
        }
    }
}
public class effect
{


    //public Stat _stat;
    public float Value;
}