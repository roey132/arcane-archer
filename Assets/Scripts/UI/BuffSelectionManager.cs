using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuffSelectionManager : MonoBehaviour
{
    [SerializeField] private List<ItemData> _items;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAllButtons()
    {
        for (int i = 0; i < 3; i++)
        {
            SetButton(transform.Find($"PickItemButton{i+1}"), _items[i]);
        }
    }

    private void SetButton(Transform buttonPrefab, ItemData item)
    {
        TextMeshProUGUI itemName = buttonPrefab.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI itemType = buttonPrefab.GetChild(1).GetComponent<TextMeshProUGUI>();
        RawImage itemIcon = buttonPrefab.GetChild(2).GetComponent<RawImage>();
        TextMeshProUGUI itemDescription = buttonPrefab.GetChild(3).GetComponent<TextMeshProUGUI>();
        Button button = buttonPrefab.GetComponent<Button>();

        itemName.text = item.ItemName;
        itemType.text = item.ItemType;
        itemIcon.texture = item.ItemIcon.texture;
        itemDescription.text = item.ItemDescription;

        button.onClick.RemoveAllListeners();

        UnityAction action1 = () => ItemManager.Instance.AddItem(item);
        UnityAction action2 = () => GameManager.Instance.HideItemPickItemUI();
        UnityAction action3 = () => WaveManager.Instance.StartWave();

        button.onClick.AddListener(action1);
        button.onClick.AddListener(action2);
        button.onClick.AddListener(action3);
    }
}
