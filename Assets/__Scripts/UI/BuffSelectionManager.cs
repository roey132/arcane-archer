using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuffSelectionManager : MonoBehaviour
{
    [SerializeField] private List<ItemData> _items;
    private void Awake()
    {
        GameManager.OnGameStateChange += SetAllButtons;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= SetAllButtons;
    }

    public void SetAllButtons(GameState state)
    {
        if (state != GameState.BuffSelection) return;
        List<ItemData> tempList = new List<ItemData>(_items);

        for (int i = 0; i < 3; i++)
        {
            int rndItem = Random.Range(0, tempList.Count);
            SetButton(transform.Find($"PickItemButton{i+1}"), tempList[rndItem]);

            tempList.RemoveAt(rndItem);
        }
    }

    private void SetButton(Transform buttonPrefab, ItemData item)
    {
        TextMeshProUGUI itemName = buttonPrefab.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI itemType = buttonPrefab.GetChild(1).GetComponent<TextMeshProUGUI>();
        Image itemIcon = buttonPrefab.GetChild(2).GetComponent<Image>();
        TextMeshProUGUI itemDescription = buttonPrefab.GetChild(3).GetComponent<TextMeshProUGUI>();
        Button button = buttonPrefab.GetComponent<Button>();

        itemName.text = item.ItemName;
        itemType.text = item.Type.ToString();
        itemIcon.sprite = item.ItemIcon;
        itemDescription.text = item.ItemDescription;

        button.onClick.RemoveAllListeners();

        UnityAction AddItem = () => ItemManager.Instance.AddItem(item);
        UnityAction HideUI = () => GameManager.Instance.HideItemPickItemUI();
        UnityAction RoomSelection = () => GameManager.Instance.UpdateGameState(GameState.RoomSelection);

        button.onClick.AddListener(AddItem);
        button.onClick.AddListener(HideUI);
        button.onClick.AddListener(RoomSelection);
    }
}
