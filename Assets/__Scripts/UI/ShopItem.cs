using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private TextMeshPro _priceText;

    private float _price;
    private ItemData _item;
    private bool _playerColliding;
    private bool _canBuy;

    void Update()
    {
        _canBuy = IngameStats.Instance.IngameCurrency >= _price;
        _priceText.color = _canBuy ? Color.green : Color.red;
        if (!_playerColliding) return;
        if (Input.GetKeyDown(KeyCode.F) && _canBuy)
        {
            BuyItem();
        }
    }

    public void InitShopItem(float price, ItemData item)
    {
        _price = price;
        _item = item;
        _priceText.text = _price.ToString();
        GetComponent<SpriteRenderer>().sprite = item.ItemIcon;
        gameObject.SetActive(true);
    }
    public void BuyItem()
    {
        IngameStats.Instance.changeIngameCurrency(-_price);
        ItemManager.Instance.AddItem( _item );
        print($"Bought Item {_item.ItemName}");
        gameObject.SetActive( false );
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Player")) return;
        _playerColliding = true;
    }
    public void OnTriggerExit2D(Collider2D collider)
    {
        if (!collider.CompareTag("Player")) return;
        _playerColliding = false;
    }

}
