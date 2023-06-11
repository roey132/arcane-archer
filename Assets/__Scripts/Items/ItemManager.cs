using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    [SerializeField] private List<ItemData> _items;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            _items = new List<ItemData>();
        }

    }

    public void AddItem(ItemData item)
    {
        _items.Add(item);
        item.ApplyItem();
    }
}
