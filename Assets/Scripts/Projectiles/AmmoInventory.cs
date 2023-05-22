using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoInventory : MonoBehaviour
{
    public static AmmoInventory Instance;
    [SerializeField] private AmmoData test1;
    [SerializeField] private AmmoData test2;

    [SerializeField] private Dictionary<AmmoData, EquippedAmmoInfo> _ammoInventory = new Dictionary<AmmoData, EquippedAmmoInfo>();
    [SerializeField] private List<GameObject> _buttons = new();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        addAmmo(test1,200);
        addAmmo(test2,200);
    }

    public bool useAmmo(AmmoData ammoData)
    {
        // do not do anything if ammoType is NormalArrow
        if (ammoData.name == "NormalArrow") return false;
        // check if there is more than 1 arrow and use it
        if (_ammoInventory[ammoData]._ammoCount > 0)
        {
            _ammoInventory[ammoData]._ammoCount -= 1;
            int currAmmoCount = _ammoInventory[ammoData]._ammoCount;
            _ammoInventory[ammoData]._button.GetComponent<AmmoButton>().UpdateAmmoCount(currAmmoCount);

            return true;
        }
        else 
        {
            _ammoInventory[ammoData]._button.GetComponent<AmmoButton>().ResetButton();
            return false; 
        }
    }
    public bool addAmmo(AmmoData ammoData, int ammoCount)
    {
        // add ammo to existing ammo inventory
        if (_ammoInventory.ContainsKey(ammoData))
        {
            _ammoInventory[ammoData]._ammoCount += ammoCount;
            int currAmmoCount = _ammoInventory[ammoData]._ammoCount;
            _ammoInventory[ammoData]._button.GetComponent<AmmoButton>().UpdateAmmoCount(currAmmoCount);
            return true;
        }
        // add new ammo to inventory unless theres already 5.
        else if (_ammoInventory.Count < 5)
        {
            foreach (GameObject button in _buttons)
            {
                if(button.activeSelf == false)
                {
                    _ammoInventory.Add(ammoData,new EquippedAmmoInfo(button, ammoCount));
                    button.GetComponent<AmmoButton>().InitButton(ammoData,ammoCount);
                    break;
                }
            }
        }
        return false;
    }
}

public class EquippedAmmoInfo
{
    public GameObject _button;
    public int _ammoCount;

    public EquippedAmmoInfo(GameObject button, int ammoCount)
    {
        _button = button;
        _ammoCount = ammoCount;
    }
}