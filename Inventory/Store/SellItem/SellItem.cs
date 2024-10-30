using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellItem : MonoBehaviour
{
    public ItemBase itemToSell;
    [SerializeField] TextMeshProUGUI textPrice;
    [SerializeField] TextMeshProUGUI SellPrice;
    [SerializeField] Button SellButton;
    [SerializeField] Button BuyButton;
    private DisplayStore displayStore;
    private void Start()
    {
        SellButton.onClick.AddListener(this.Sell);
        BuyButton.onClick.AddListener(this.Buy);
        displayStore = GameObject.Find("StoreDisplay").GetComponent<DisplayStore>();
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = itemToSell.name;
        textPrice.text = itemToSell.price.ToString();
        SellPrice.text = itemToSell.SellPrice.ToString();

    }
    public void Sell()
    {
        displayStore.SellItem(itemToSell);
    }
    public void Buy()
    {
        displayStore.BuyItem(itemToSell);
    }
}
