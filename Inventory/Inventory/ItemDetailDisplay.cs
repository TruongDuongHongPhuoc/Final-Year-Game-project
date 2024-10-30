using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemDetailDisplay : MonoBehaviour
{   
    private GameObject UIDescription;
    [SerializeField] ItemBase itemToDisplay;
    private Inventory playerInventory;
    private void Start() {
        playerInventory = PlayerController.intance.inventory;
        UIDescription = GameObject.Find("ItemDescriptionText");
    }
    public void SetDescription(){
        if(itemToDisplay != null){
            UIDescription.gameObject.GetComponent<TextMeshProUGUI>().text = "Description: " + itemToDisplay.description;
        }
    }
    public void GetItemToDisplay(ItemBase itemBase){
        this.itemToDisplay = itemBase;
        SetDescription();
    }
    public void UseButton(){
        if(playerInventory != null && itemToDisplay != null){
            AudioManager.instance.PlayAudioHaveName("Success");
            itemToDisplay.UseOfItem();
            playerInventory.MinusAmountItem(itemToDisplay, -1);
        }
    }
    public void RemoveItem(){
        playerInventory.RemoveItem(itemToDisplay);
    }

}
