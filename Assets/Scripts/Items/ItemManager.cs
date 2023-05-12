using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private List<ItemData> _items;

    void Start()
    {
        _items = new List<ItemData>();
    }

    void Update()
    {
        
    }
    public void AddItem(ItemData item)
    {
        _items.Add(item);
        item.ApplyItem();
    }
}
