using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    public static DisplayInventory instance
    {
        get; private set;
    }
    public ItemType FillType;
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject storeUI;
    Dictionary<InventorySlot, GameObject> DisplayedItem = new Dictionary<InventorySlot, GameObject>();
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        DisplayItem();
    }
    private void Update()
    {
        UpdateInventory();
        CheckItemExist();
    }
    public void DisplayItem()
    {
        foreach (InventorySlot slot in inventory.container)
        {
            if (FillType != ItemType.All){
                if (slot.itemBase.ItemType == FillType)
                {
                    DisplayItem(slot);
                }
                }
                else
                {
                    DisplayItem(slot);
                }
        }
    }
    public void UpdateInventory()
    {
        GameObject CurrentPrefab;
        foreach (InventorySlot slot in inventory.container)
        {
            if (CheckInventorySlot(slot))
            {
                DisplayedItem.TryGetValue(slot, out CurrentPrefab);
                if(CurrentPrefab != null){
                // TextMeshProUGUI UIName = CurrentPrefab.GetComponentInChildren<TextMeshProUGUI>();
                // UIName.text = slot.itemBase.name;
                // UIName.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "X" + slot.amount.ToString();
                if(CurrentPrefab.transform.GetChild(3).GetChild(0).GetComponentInChildren<TextMeshProUGUI>() == null){
                    Destroy(CurrentPrefab.transform.GetChild(3).GetChild(0));
                }else{
                CurrentPrefab.transform.GetChild(3).GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = "X" + slot.amount.ToString();
                }
                }else{
                    // DisplayItem();
                    Debug.LogError("UI of "+ slot.itemBase +"is not created");
                }
            }
            else
            {
                if (FillType != ItemType.All){
                if (slot.itemBase.ItemType == FillType)
                {
                    DisplayItem(slot);
                }
                }
                else
                {
                    DisplayItem(slot);
                }
            }
        }
    }
    private bool CheckInventorySlot(InventorySlot inventorySlot)
    {
        if (DisplayedItem.ContainsKey(inventorySlot))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void DisplayItem(InventorySlot slot)
    {
        GameObject prefab = Instantiate(slot.itemBase.UIprefab, storeUI.transform);
        TextMeshProUGUI text = prefab.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null)
        {
            text.text = slot.itemBase.name;
            // text.text = slot.amount.ToString();
        }
        DisplayedItem.Add(slot, prefab);
    }
    public void CheckItemExist()
    {
        List<InventorySlot> itemsToRemove = new List<InventorySlot>();

        foreach (InventorySlot inventorySlot in DisplayedItem.Keys)
        {
            bool exist = inventory.container.Contains(inventorySlot);
            if (!exist)
            {
                // Add the inventorySlot to the list of items to remove
                itemsToRemove.Add(inventorySlot);
            }
        }

        // Remove the items from the dictionary
        foreach (InventorySlot itemToRemove in itemsToRemove)
        {
            GameObject objToRemove;
            if (DisplayedItem.TryGetValue(itemToRemove, out objToRemove))
            {
                Destroy(objToRemove);
                DisplayedItem.Remove(itemToRemove);
            }
        }
    }

}
