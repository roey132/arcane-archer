using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ingameStats : MonoBehaviour
{
    [SerializeField] private playerBaseData _baseData;

    public float _maxHp { get; private set; }
    public float _currHp { get; private set; }
    public float _maxMana { get; private set; }
    public float _currMana { get; private set; }
    public float _attackSpeed { get; private set; }
    public float _castSpeed { get; private set; }
    public float _movementSpeed { get; private set; }
    public float _criticalRate { get; private set; }
    public float _criticalDamage { get; private set; }
    public float _physicalDamageMultiplier { get; private set; }
    public float _magicalDamageMultiplier { get; private set; }
    public float _numOfArrows { get; private set; }
    void Awake()
    {
        _baseData = transform.parent.Find("playerBaseStats").GetComponent<playerBaseData>();

        _maxHp = _baseData._health;
        _currHp = _baseData._health;
        _maxMana = _baseData._mana;
        _currMana = _baseData._mana;
        _attackSpeed = _baseData._attackSpeed;
        _castSpeed = _baseData._castSpeed;
        _movementSpeed = _baseData._movementSpeed;
        _criticalRate = _baseData._criticalRate;
        _criticalDamage = _baseData._criticalDamage;
        _physicalDamageMultiplier = _baseData._physicalDamageMultiplier;
        _magicalDamageMultiplier = _baseData._magicalDamageMultiplier;
        _numOfArrows = _baseData._numOfArrows;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hitPlayer(float damage)
    {
        _currHp -= damage;
        print($"player was hit for {damage} damage");
    }
    public bool useMana(float mana)
    {
        if (_currMana - mana >= 0) 
        { 
            _currMana -= mana;
            return true;
        }
        else return false;
    }
    public void gainMana(float mana)
    {
        float newMana = _currMana + mana;
        _currMana = Mathf.Clamp(newMana, 0, _maxMana);
    }
    public void changeAttackSpeed(float attackSpeed)
    {
        _attackSpeed += attackSpeed;
    }
    public void changeCastSpeed(float castSpeed)
    {
        _castSpeed += castSpeed;
    }
    public void changeMovementSpeed(float movementSpeed)
    {
        _movementSpeed += movementSpeed;
    }
    public void changeCriticalRate(float critRate)
    {
        _criticalRate += critRate;
    }
    public void changeCriticalDamage(float critDamage)
    {
        _criticalDamage += critDamage;
    }
    public void changePhysicalMultipler(float multiplier)
    {
        _physicalDamageMultiplier += multiplier;
    }
    public void changeMagicalMultiplier(float multiplier)
    {
        _magicalDamageMultiplier += multiplier;
    }
    public void changeNumOfArrows(float numOfArrows)
    {
        _numOfArrows += numOfArrows;
    }
}
