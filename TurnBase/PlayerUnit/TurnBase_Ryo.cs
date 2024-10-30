using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBase_Ryo : UnitBase
{
    PlayerCharacterEquipment PCE;
    private void Start() {
        // PlayerCondition playerCondition = PlayerController.intance.playerCondition;
        if(animator == null)
        animator = gameObject.GetComponentInChildren<Animator>();
        // if(PCE == null){
        //    PCE = gameObject.AddComponent<PlayerCharacterEquipment>();
        //    PCE.PlayerSkin = this.gameObject;
        //    PCE.FindPlayerBones();
        //    if(playerCondition.helmet != null){
        //    PCE.Helmetprefab = playerCondition.helmet.equipPrefab; 
        //    }
        //    if(playerCondition.Chest != null){
        //    PCE.ChestPlaceprefab = playerCondition.Chest.equipPrefab; 
        //    }
        //    if(playerCondition.pant != null){
        //    PCE.PantArmorprefab = playerCondition.pant.equipPrefab; 
        //    }
        //    PCE.EquipAllArmor();
        // }
    }
   
}
