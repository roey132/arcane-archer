using TMPro;
using UnityEngine;
using UnityEngine.Android;

public class IngameStats : MonoBehaviour
{
    public static IngameStats Instance;

    [SerializeField] private playerBaseData _baseData;

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
    // TODO temporary set currency UI through this code, make a UI manager later on
    [SerializeField] private TextMeshProUGUI currencyText;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        MaxHp = _baseData.Health;
        CurrHp = _baseData.Health;
        MaxMana = _baseData.Mana;
        CurrMana = _baseData.Mana;
        AttackSpeed = _baseData.AttackSpeed;
        CastSpeed = _baseData.CastSpeed;
        MovementSpeed = _baseData.MovementSpeed;
        CriticalRate = _baseData.CriticalRate;
        CriticalDamage = _baseData.CriticalDamage;
        PhysicalDamageMultiplier = _baseData.PhysicalDamageMultiplier;
        MagicalDamageMultiplier = _baseData.MagicalDamageMultiplier;
        BaseDamage = _baseData.BaseDamage;
        BaseMagicDamage = _baseData.BaseMagicDamage;
        NumOfArrows = _baseData.NumOfArrows;
        IngameCurrency = _baseData.StartingIngameCurrency;
    }
    void Start()
    {

    }
    void Update()
    {
        currencyText.text = IngameCurrency.ToString();
    }

    public void hitPlayer(float damage)
    {
        CurrHp -= damage;
        print($"player was hit for {damage} damage");
    }
    public bool useMana(float mana)
    {
        if (CurrMana - mana >= 0)
        {
            CurrMana -= mana;
            return true;
        }
        else return false;
    }
    public void gainMana(float mana)
    {
        float newMana = CurrMana + mana;
        CurrMana = Mathf.Clamp(newMana, 0, MaxMana);
    }
    public void changeAttackSpeed(float attackSpeed)
    {
        AttackSpeed += attackSpeed;
    }
    public void changeCastSpeed(float castSpeed)
    {
        CastSpeed += castSpeed;
    }
    public void changeMovementSpeed(float movementSpeed)
    {
        MovementSpeed += movementSpeed;
    }
    public void changeCriticalRate(float critRate)
    {
        CriticalRate += critRate;
    }
    public void changeCriticalDamage(float critDamage)
    {
        CriticalDamage += critDamage;
    }
    public void changePhysicalMultipler(float multiplier)
    {
        PhysicalDamageMultiplier += multiplier;
    }
    public void changeMagicalMultiplier(float multiplier)
    {
        MagicalDamageMultiplier += multiplier;
    }
    public void changeNumOfArrows(float numOfArrows)
    {
        NumOfArrows += numOfArrows;
    }
    public bool changeIngameCurrency(int currency)
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
}