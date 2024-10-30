using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectTurn : BaseTurn
{
    public override void TurnFlow(TurnBaseManager turnBaseManager)
    {
         turnBaseManager.animator.SetTrigger("PlayerEffect");
       turnBaseManager.PlayerCharacter.EffectDeal();
       //text show
       if(turnBaseManager.EnemyCharacter.currentEffect != null ){
       string text = turnBaseManager.PlayerCharacter.UnitName + " Suffer from " + turnBaseManager.PlayerCharacter.currentEffect;
        turnBaseManager.UI.ShowDialogue(text);
       }else{
        turnBaseManager.staticSwitchTurn(turnBaseManager.playerInputTurn);
       }
       turnBaseManager.SwitchTurn(turnBaseManager.playerInputTurn);
    }

}
