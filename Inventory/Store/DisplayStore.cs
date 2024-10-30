using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStore : MonoBehaviour
{
    public Inventory currentStore;
    public GameObject basicPrefab;
    [SerializeField] GameObject container;
    private PlayerCondition playerCondition;
    private Inventory playerInventory;
    List<GameObject> itemDisplayed = new List<GameObject>();
    private void Start() {
        playerCondition = PlayerController.intance.playerCondition;
         playerInventory = PlayerController.intance.inventory;
    }
    public void DisplayTheStore(){
        foreach(InventorySlot item in currentStore.container){
            Debug.Log("create new prefab");
            itemDisplayed.Add(Instantiate(SetupPrefab(item.itemBase),container.transform));
        }
    }

    public void DeactiveStore(){
       this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
       if(itemDisplayed.Count > 0){
        foreach(GameObject g in itemDisplayed){
            Destroy(g);
        }
        itemDisplayed.Clear();
       }
    }
    public void SellItem(ItemBase itemBase){
        if(playerInventory.CheckItemExist(itemBase)){
            playerInventory.MinusAmountItem(itemBase, -1);
            playerCondition.Money += itemBase.SellPrice;
        }else{
            Debug.Log("Item is not in your inventory");
        }
    }
    public void BuyItem(ItemBase itemBase){
        if(playerCondition.Money <= itemBase.price){
            Debug.Log("Not Enough Money");
        }
        playerInventory.AddItem(itemBase,1);
        playerCondition.Money -= itemBase.price;  
    }
    public GameObject SetupPrefab(ItemBase item){
        basicPrefab.transform.GetChild(1).GetComponent<Image>().sprite = item.displayImg;// display image store 
        basicPrefab.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.name +": "+ item.description;
        basicPrefab.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = item.price.ToString();
        basicPrefab.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = item.SellPrice.ToString();
        //button
        basicPrefab.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(() => SellItem(item));
        basicPrefab.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(() => BuyItem(item));
        return basicPrefab;
    }
}
