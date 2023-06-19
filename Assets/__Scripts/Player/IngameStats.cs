using TMPro;
using UnityEngine;
using UnityEngine.Android;

public class IngameStats : MonoBehaviour
{
    public static IngameStats Instance;

    public float MaxHp { get; private set; }
    public float CurrHp { get; private set; }
    public float MaxMana { get; private set; }
    public float CurrMana { get; private set; }
    public float AttackSpeed { get; private set; }
    public float CastSpeed { get; private set; }
    public float MovementSpeed { get; private set; }
    public float CriticalRate { get; private set; }
    public float CriticalDamage { get; private set; }
    public float PhysicalDamageMultiplier { get; private set; }
    public float MagicalDamageMultiplier { get; private set; }
    public float NumOfArrows { get; private set; }
    public float BaseDamage { get; private set; }
    public float BaseMagicDamage { get; private set; }
    public float IngameCurrency { get; private set; }

    public AmmoData EquippedAmmo = null;
    // TODO temporary set currency UI through this code, make a UI manager later on
    //[SerializeField] private TextMeshProUGUI currencyText;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        MaxHp = PlayerBaseData.Instance.Health;
        CurrHp = PlayerBaseData.Instance.Health;
        MaxMana = PlayerBaseData.Instance.Mana;
        CurrMana = PlayerBaseData.Instance.Mana;
        AttackSpeed = PlayerBaseData.Instance.AttackSpeed;
        CastSpeed = PlayerBaseData.Instance.CastSpeed;
        MovementSpeed = PlayerBaseData.Instance.MovementSpeed;
        CriticalRate = PlayerBaseData.Instance.CriticalRate;
        CriticalDamage = PlayerBaseData.Instance.CriticalDamage;
        PhysicalDamageMultiplier = PlayerBaseData.Instance.PhysicalDamageMultiplier;
        MagicalDamageMultiplier = PlayerBaseData.Instance.MagicalDamageMultiplier;
        BaseDamage = PlayerBaseData.Instance.BaseDamage;
        BaseMagicDamage = PlayerBaseData.Instance.BaseMagicDamage;
        NumOfArrows = PlayerBaseData.Instance.NumOfArrows;
        IngameCurrency = PlayerBaseData.Instance.StartingIngameCurrency;
    }
    void Update()
    {

    }

    public void HitPlayer(float damage)
    {
        
        CurrHp -= Mathf.RoundToInt(damage);
        print($"player was hit for {damage} damage");
    }
    public bool UseMana(float mana)
    {
        if (CurrMana - mana >= 0)
        {
            CurrMana -= mana;
            return true;
        }
        else return false;
    }
    public void GainMana(float mana)
    {
        float newMana = CurrMana + mana;
        CurrMana = Mathf.Clamp(newMana, 0, MaxMana);
    }
    public void ChangeAttackSpeed(float attackSpeed)
    {
        AttackSpeed += attackSpeed;
    }
    public void ChangeCastSpeed(float castSpeed)
    {
        CastSpeed += castSpeed;
    }
    public void ChangeMovementSpeed(float movementSpeed)
    {
        MovementSpeed += movementSpeed;
    }
    public void ChangeCriticalRate(float critRate)
    {
        CriticalRate += critRate;
    }
    public void ChangeCriticalDamage(float critDamage)
    {
        CriticalDamage += critDamage;
    }
    public void ChangePhysicalMultipler(float multiplier)
    {
        PhysicalDamageMultiplier += multiplier;
    }
    public void ChangeMagicalMultiplier(float multiplier)
    {
        MagicalDamageMultiplier += multiplier;
    }
    public void ChangeNumOfArrows(float numOfArrows)
    {
        NumOfArrows += numOfArrows;
    }
    public bool ChangeIngameCurrency(float currency)
    {
        // add currency if the currency value is above 0
        if (currency > 0)
        {
            IngameCurrency += currency;
            return true;
        }
        else
        {
            // return false if the player can't use X amount of currency 
            if (IngameCurrency + currency < 0)
            {
                return false;
            }
            // return true if the player has enough currency to use
            else
            {
                IngameCurrency += currency;
                return true;
            }
        }
    }
    public void SetEquippedAmmo(AmmoData ammo)
    {
        EquippedAmmo = ammo;
    }
}