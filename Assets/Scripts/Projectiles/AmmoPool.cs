using System.Collections.Generic;
using UnityEngine;

public class AmmoPool : MonoBehaviour
{
    public static AmmoPool Instance;

    [SerializeField] private GameObject _AmmoPrefab;
    private List<GameObject> _ammoPool = new List<GameObject>();

    public int AmountToPool = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }
    void Start()
    {
        for (int i = 0; i < AmountToPool; i++)
        {
            GameObject ammo = Instantiate(_AmmoPrefab,transform);
            ammo.SetActive(false);
            _ammoPool.Add(ammo);
        }
    }
    public GameObject GetAmmoPrefab() 
    {
        for ( int i = 0;i < _ammoPool.Count; i++)
        {
            if (!_ammoPool[i].activeInHierarchy)
            {
                return _ammoPool[i];
            }
        }
        return null;
    }
}
