using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSelectInventory : MonoBehaviour
{
    private ItemDetailDisplay itemDetailDisplay;
    [SerializeField] ItemBase itemToDisplay;
    private void Start() {
        itemDetailDisplay = GameObject.Find("DisplayItem").GetComponent<ItemDetailDisplay>();
    }
    public void ButtonLogic(){
        itemDetailDisplay.GetItemToDisplay(itemToDisplay);
    }
}
