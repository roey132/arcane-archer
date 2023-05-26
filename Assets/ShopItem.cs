using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private float _price;
    [SerializeField] private TextMeshPro _priceText;
    [SerializeField] private ItemData _item;
    private bool _playerColliding;
    private bool _canBuy;
    // Start is called before the first frame update
    void Start()
    {
        InitShopItem(_price, _item);
    }

    // Update is called once per frame
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
    }
    public void BuyItem()
    {
        ItemManager.Instance.AddItem( _item );
        gameObject.SetActive( false );
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        print("Player Colliding");
        if (Input.GetKeyDown(KeyCode.F))
        {
            print("Pressed Enter");
            BuyItem();
        }
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
