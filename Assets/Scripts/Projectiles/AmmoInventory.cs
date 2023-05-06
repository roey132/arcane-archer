using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoInventory : MonoBehaviour
{
    public static AmmoInventory Instance;

    [SerializeField] private Dictionary<string, int> _ammoInventory = new Dictionary<string, int>();
    [SerializeField] public TextMeshProUGUI _fireAmmoCount;
    [SerializeField] public TextMeshProUGUI _iceAmmoCount;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        _ammoInventory.Add("FireBomb", 50);
        _ammoInventory.Add("IceBomb", 50);
    }

    // Update is called once per frame
    void Update()
    {
        _fireAmmoCount.text = _ammoInventory["FireBomb"].ToString();
        _iceAmmoCount.text = _ammoInventory["IceBomb"].ToString();
    }
    public bool useAmmo(string ammoType)
    {
        // do not do anything if ammoType is NormalArrow
        if (ammoType == "NormalArrow") return false;
        // check if there is more than 1 arrow and use it
        if (_ammoInventory[ammoType] > 0)
        {
            _ammoInventory[ammoType] -= 1;
            return true;
        }
        else 
        { 
            return false; 
        }
    }
    public bool addAmmo(string ammoType, int ammoCount)
    {
        // add ammo to existing ammo inventory
        if (_ammoInventory.ContainsKey(ammoType))
        {
            _ammoInventory[ammoType] += ammoCount;
            return true;
        }
        // add new ammo to inventory unless theres already 5.
        else if (_ammoInventory.Count < 5)
        {
            _ammoInventory.Add(ammoType, ammoCount);
        }
        return false;
    }
    // temporary for test buttons UI
    public void addAmmoButton(string ammoType)
    {
        _ammoInventory[ammoType] += 50;
    }

}
