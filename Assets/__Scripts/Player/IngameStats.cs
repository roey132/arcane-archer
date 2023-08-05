using TMPro;
using UnityEngine;
using UnityEngine.Android;

public class IngameStats : MonoBehaviour
{
    public static IngameStats Instance;

    // basic stats
    public float MaxHp { get; private set; }
    public float CurrHp { get; private set; }
    public float AttackSpeed { get; private set; }
    public float MovementSpeed { get; private set; }
    public float CriticalRate { get; private set; }
    public float NumOfArrows { get; private set; }
    public float Damage { get; private set; }
    public float MagicDamage { get; private set; }
    public float IngameCurrency { get; private set; }

    // multiplier stats
    public float DamageMultiplier { get; private set; }
    public float MagicalDamageMultiplier { get; private set; }
    public float CurrencyGainMultiplier { get; private set; }
    public float CriticalDamageMultiplier { get; private set; }

    // elemental stats
    public float FireDamageMultiplier { get; private set; }
    public float IceDamageMultiplier { get; private set; }

    public ArrowData EquippedArrow = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        // init basic stats
        MaxHp = PlayerBaseData.Instance.Health;

        CurrHp = PlayerBaseData.Instance.Health;

        AttackSpeed = PlayerBaseData.Instance.AttackSpeed;

        MovementSpeed = PlayerBaseData.Instance.MovementSpeed;

        CriticalRate = PlayerBaseData.Instance.CriticalRate;

        Damage = PlayerBaseData.Instance.BaseDamage;

        MagicDamage = PlayerBaseData.Instance.BaseMagicDamage;

        NumOfArrows = PlayerBaseData.Instance.NumOfArrows;

        // init multipliers
        CriticalDamageMultiplier = PlayerBaseData.Instance.CriticalDamageMultiplier;

        DamageMultiplier = PlayerBaseData.Instance.PhysicalDamageMultiplier;

        MagicalDamageMultiplier = PlayerBaseData.Instance.MagicalDamageMultiplier;

        CurrencyGainMultiplier = PlayerBaseData.Instance.CurrencyMultiplier;

        // Init Elements
        FireDamageMultiplier = PlayerBaseData.Instance.FireDamageMultiplier;

        IceDamageMultiplier = PlayerBaseData.Instance.IceDamageMultiplier;

        // init currency
        IngameCurrency = PlayerBaseData.Instance.StartingIngameCurrency;

    }
    public void ChangeMaxHp(float health)
    {
        MaxHp += health;
        CurrHp += health;

        print($"STATS INFO change max hp by {health}");
    }
    public void HitPlayer(float damage)
    {
        CurrHp -= Mathf.RoundToInt(damage);
        print($"DAMAGE INFO player was hit for {damage} damage");
    }
    public void ChangeAttackSpeed(float attackSpeed)
    {
        AttackSpeed += attackSpeed;
        print($"STATS INFO changed attack speed by {attackSpeed}");
    }
    public void ChangeMovementSpeed(float movementSpeed)
    {
        MovementSpeed += movementSpeed;
        print($"STATS INFO changed movement speed by {movementSpeed}");
    }
    public void ChangeCriticalRate(float critRate)
    {
        CriticalRate += critRate;
        print($"STATS INFO changed crit rate by {critRate}");
    }
    public void ChangeCriticalDamageMultiplier(float critDamage)
    {
        CriticalDamageMultiplier += critDamage;
        print($"STATS INFO changed crit damage multiplier by {critDamage}");
    }
    public void ChangeDamage(float damage)
    {
        Damage += damage;
        print($"STATS INFO changed damage by {damage}");
    }
    public void ChangeDamageMultipler(float multiplier)
    {
        DamageMultiplier += multiplier;
        print($"STATS INFO changed damage multiplier by {multiplier}");
    }
    public void ChangeMagicalDamage(float magicalDamage)
    {
        MagicDamage += magicalDamage;
        print($"STATS INFO changed magical damage by {magicalDamage}");
    }
    public void ChangeMagicalDamageMultiplier(float multiplier)
    {
        MagicalDamageMultiplier += multiplier;
        print($"STATS INFO changed magical damage multiplier by {multiplier}");
    }
    public void ChangeNumOfArrows(float numOfArrows)
    {
        NumOfArrows += numOfArrows;
        print($"STATS INFO changed number of arrows by {numOfArrows}");
    }
    public bool ChangeIngameCurrency(float currency)
    {
        // add currency if the currency value is above 0
        if (currency > 0)
        {
            IngameCurrency += currency * CurrencyGainMultiplier;
            print($"STATS INFO gained {currency} currency");
            return true;
        }
        else
        {
            // return false if the player can't use X amount of currency 
            if (IngameCurrency + currency < 0)
            {
                print($"STATS INFO not enough currency | current currency is {IngameCurrency}, price is {Mathf.Abs(currency)}");
                return false;
            }
            // return true if the player has enough currency to use, reduce that currency
            else
            {
                IngameCurrency += currency;
                print($"STATS INFO used {Mathf.Abs(currency)} currency");
                return true;
            }
        }
    }
    public void ChangeFireDamageMultiplier(float multiplier)
    {
        FireDamageMultiplier += multiplier;
        print($"STATS INFO changed fire damage multiplier by {multiplier}");
    }
    public void ChangeIceDamageMultiplier(float multiplier)
    {
        IceDamageMultiplier += multiplier;
        print($"STATS INFO changed ice damage multiplier by {multiplier}");
    }
    public void ChangeCurrencyGainMultiplier(float multiplier)
    {
        CurrencyGainMultiplier += multiplier;
        print($"STATS INFO changed CurrencyGainMultiplier by {multiplier}");
    }
    public void SetEquippedArrow(ArrowData arrow)
    {
        EquippedArrow = arrow;
    }
}