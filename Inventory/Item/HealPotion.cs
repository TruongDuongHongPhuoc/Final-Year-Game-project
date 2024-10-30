using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Postion Object", menuName ="Inventory system/Item/Potion")]
public class HealPotion : ItemBase
{
    public int minHealAmount;
    public int maxHealAmount;
    private int healAmount;
    PlayerController playerController;
    public override void UseOfItem()
    {
       healing();
    }
    public void healing(){
       healAmount = Random.Range(minHealAmount, maxHealAmount + 1);
      if(playerController == null){
         FindPlayer();
      }
       playerController.playerCondition.CurrentCharacter.useUnit.CurrentHeal += healAmount;
       Debug.Log("Player heal for" + healAmount);
    }
    public void FindPlayer(){
       playerController = PlayerController.intance;
    }
}
