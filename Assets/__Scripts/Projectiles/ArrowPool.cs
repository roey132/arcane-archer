using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    public static ArrowPool Instance;

    [SerializeField] private GameObject _arrowPrefab;
    private List<GameObject> _arrowPool = new();

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
            GameObject arrow = Instantiate(_arrowPrefab,transform);
            arrow.SetActive(false);
            _arrowPool.Add(arrow);
        }

    }
    public GameObject GetAmmoPrefab() 
    {
        for ( int i = 0;i < _arrowPool.Count; i++)
        {
            if (!_arrowPool[i].activeInHierarchy)
            {
                return _arrowPool[i];
            }
        }
        return null;
    }
}
