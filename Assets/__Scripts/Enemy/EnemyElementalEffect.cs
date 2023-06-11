using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


public enum DebuffElement
{
    noEffect,
    fire,
    ice
}
public static class EnemyElementalDebuffs
{
    public static void ApplyDebuff(Enemy enemy,DebuffData debuff)
    {
        switch(debuff.Element) 
        {
            case DebuffElement.fire:
                ApplyFire(enemy, debuff);
                break;
            case DebuffElement.ice:
                ApplyIce(enemy, debuff);
                break;
        }
    }
    public static void ApplyFire(Enemy enemy, DebuffData debuff)
    {
        Debug.Log($"{debuff.Element} Ticked {Time.time}");
        enemy.Hit(debuff.DebuffEffects["TickDamage"],"arrow");
        enemy.SetColor(debuff.debuffColor);
    }
    public static void ApplyIce(Enemy enemy, DebuffData debuff)
    {
        Debug.Log($"{debuff.Element} Ticked {Time.time}");
        enemy.Hit(debuff.DebuffEffects["TickDamage"], "arrow");
        enemy.SlowPct(debuff.DebuffEffects["SlowPct"]);
        enemy.SetColor(debuff.debuffColor);
    }
}
