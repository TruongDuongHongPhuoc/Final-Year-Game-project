using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownTurn : BaseTurn
{

    public override void TurnFlow(TurnBaseManager turnBaseManager)
    {
        turnBaseManager.animator.SetTrigger("CoolDown");
        if(turnBaseManager.PlayerCharacter.currentEffect !=null){
       turnBaseManager.PlayerCharacter.currentEffect.TurnRemainDecrease(turnBaseManager.PlayerCharacter);
        }
        if(turnBaseManager.EnemyCharacter.currentEffect!=null){
       turnBaseManager.EnemyCharacter.currentEffect.TurnRemainDecrease(turnBaseManager.EnemyCharacter);
        }
       turnBaseManager.SwitchTurn(turnBaseManager.playerEffectTurn);

       if(turnBaseManager.EnemyCharacter == null){
        turnBaseManager.SwitchTurn(turnBaseManager.initialTurn);
       }
    }

}
