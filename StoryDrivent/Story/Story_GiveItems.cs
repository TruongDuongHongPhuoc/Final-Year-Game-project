using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Story_GiveItems : EventStoryBase
{
    [SerializeField] ItemBase itemToTake;
    [SerializeField] int quantity;
    public override void EventHandle()
    {
        Inventory playerinventory = PlayerController.intance.inventory;
        if(quantity >0){
            playerinventory.AddItem(itemToTake, quantity);
        }else if(playerinventory.ReturnAmount(itemToTake) >= quantity){
            playerinventory.AddAmount(itemToTake, quantity);
        }
    }
}
