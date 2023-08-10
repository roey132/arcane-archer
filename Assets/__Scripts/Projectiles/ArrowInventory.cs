using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArrowInventory : MonoBehaviour
{
    public static ArrowInventory Instance;

    [SerializeField] private Dictionary<ArrowData, EquippedArrowInfo> _arrowInventory = new Dictionary<ArrowData, EquippedArrowInfo>();
    [SerializeField] private List<GameObject> _buttons = new();
    [SerializeField] private GameObject _equippedArrowIndicator;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {

    }

    public void UseArrow(ArrowData arrowData)
    {
        // do not do anything if ammoType is NormalArrow
        if (arrowData.name == "NormalArrow") return;
        if (!_arrowInventory.ContainsKey(arrowData)) return;

        // check if there is more than 1 arrow and use it
        if (_arrowInventory[arrowData]._arrowCount > 0)
        {
            _arrowInventory[arrowData]._arrowCount -= 1;
            int currAmmoCount = _arrowInventory[arrowData]._arrowCount;
            _arrowInventory[arrowData]._button.GetComponent<ArrowButton>().UpdateAmmoCount(currAmmoCount);
        }
        if (_arrowInventory[arrowData]._arrowCount == 0)
        {
            _arrowInventory[arrowData]._button.GetComponent<ArrowButton>().ResetButton();
            _arrowInventory.Remove(arrowData);
            _equippedArrowIndicator.SetActive(false);
            IngameStats.Instance.SetEquippedArrow(null);
        }
    }
    public bool AddArrow(ArrowData arrowData, int ammoCount)
    {
        // add arrow to existing arrow inventory
        if (_arrowInventory.ContainsKey(arrowData))
        {
            _arrowInventory[arrowData]._arrowCount += ammoCount;
            int currAmmoCount = _arrowInventory[arrowData]._arrowCount;
            _arrowInventory[arrowData]._button.GetComponent<ArrowButton>().UpdateAmmoCount(currAmmoCount);
            return true;
        }
        // add new arrow to inventory unless theres already 5.
        else if (_arrowInventory.Count < 5)
        {
            foreach (GameObject button in _buttons)
            {
                if(button.activeSelf == false)
                {
                    // create arrow pool
                    ArrowPools.Instance.CreateArrowPool(arrowData);

                    // add arrow to the inventory dictionary
                    _arrowInventory.Add(arrowData, new EquippedArrowInfo(button, ammoCount));

                    // init the button
                    button.SetActive(true);
                    button.GetComponent<ArrowButton>().InitButton(arrowData, ammoCount);
                    break;
                }
            }
        }
        return false;
    }
}

public class EquippedArrowInfo
{
    public GameObject _button;
    public int _arrowCount;

    public EquippedArrowInfo(GameObject button, int arrowCount)
    {
        _button = button;
        _arrowCount = arrowCount;
    }
}