using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory system/ New Inventory")]
public class Inventory : ScriptableObject
{
    public List<InventorySlot> container = new List<InventorySlot>();
    public void AddItem(ItemBase itemBase, int amount)
    {
       
        if (CheckItemExist(itemBase))
        {
            AddAmount(itemBase, amount);
        }
        else
        {
            container.Add(new InventorySlot(itemBase, amount));
        }
        if(amount > 0){
        AudioManager.instance.PlayAudioHaveName("Success");
        NotifyPlayer.intance.showNotify(" an " + itemBase.name + "just added to your inventory" + amount);
        }else{
        AudioManager.instance.PlayAudioHaveName("Success");
            NotifyPlayer.intance.showNotify(" an " + itemBase.name + "just Remove to your inventory: " + amount);
        }
    }
    public void MinusAmountItem(ItemBase _item, int amount)
    {
        AddItem(_item, amount);
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].itemBase == _item && container[i].amount <= 0)
            {
                container.RemoveAt(i);
            }
        }
    }
    public void RemoveItem(ItemBase itemToRemove)
    {
        if (CheckItemExist(itemToRemove))
        {
            for (int i = 0; i < container.Count; i++)
            {
                if (container[i].itemBase == itemToRemove || container[i].itemBase.Equals(itemToRemove))
                {
                    container.RemoveAt(i);
                    AudioManager.instance.PlayAudioHaveName("Success");
                    NotifyPlayer.intance.showNotify(" an " + itemToRemove.name + "just Remove to your inventory entirely");
                }
            }
            
        }
    }
    public int ReturnAmount(ItemBase _item)
    {
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].itemBase == _item)
            {
                int quality = container[i].amount;
                return quality;
            }
        }
        return 0;
    }
    public bool CheckItemExist(ItemBase itemToCheck)
    {
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].itemBase == itemToCheck || container[i].itemBase.Equals(itemToCheck))
            {
                return true;
            }
        }
        return false;
    }
    public void AddAmount(ItemBase itemBase, int amount)
    {
        foreach (InventorySlot slot in container)
        {
            if (slot.itemBase == itemBase)
            {
                slot.addAmount(amount);
            }
        }
    }
    public int ReturnPostionItem(ItemBase itemToReturn){
        for (int i = 0; i < container.Count;i++){
            if (container[i].itemBase == itemToReturn || container[i].itemBase.Equals(itemToReturn)){
                return i;
            }
        }
        return -1;
    }
    
}



[Serializable]
public class InventorySlot
{
        public ItemBase itemBase;
    public int amount;
    public InventorySlot(ItemBase _itemBase, int _amount)
    {
        this.itemBase = _itemBase;
        this.amount = _amount;
    }
    public void addAmount(int addAmount)
    {
        this.amount += addAmount;
    }

}
