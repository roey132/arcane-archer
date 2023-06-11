using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private List<ItemData> _items;
    [SerializeField] private int _numOfItems;
    void Awake()
    {
        GameManager.OnGameStateChange += ActivateShop;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= ActivateShop;
    }

    public void ActivateShop(GameState state)
    {
        if (state != GameState.ShopRoom) 
        {
            gameObject.SetActive(false);
            return;
        };
        gameObject.SetActive(true);

        transform.Find("RoomSelectionActivator").gameObject.SetActive(true);

        print(_items.RandomItems(3));

        List<ItemData> tempList = new List<ItemData>(_items);
        for (int i = 0; i < _numOfItems; i++)
        {
            int rndItem = Random.Range(0, tempList.Count);
            transform.Find($"ShopItemObject{i + 1}").GetComponent<ShopItem>().InitShopItem(tempList[rndItem].ItemBasePrice, tempList[rndItem]);

            tempList.RemoveAt(rndItem);
        }
    }
}
