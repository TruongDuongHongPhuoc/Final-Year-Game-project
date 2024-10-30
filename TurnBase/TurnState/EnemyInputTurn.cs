using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInputTurn : BaseTurn
{


    public override void TurnFlow(TurnBaseManager turnBaseManager)
    {
         turnBaseManager.animator.SetTrigger("EnemyInput");
      if(turnBaseManager.EnemyCharacter.isAttackble){
       turnBaseManager.EnemyCharacter.AttackEnemy(turnBaseManager.EnemyCharacter.ramdomizeSkill().skillName,turnBaseManager.PlayerCharacter);
      //text show
       
       string text = turnBaseManager.EnemyCharacter.UnitName + " Attack " + turnBaseManager.PlayerCharacter.UnitName;
        turnBaseManager.UI.ShowDialogue(text);
      }else{
        Debug.Log("Enemy is not attackable");
      }

       turnBaseManager.SwitchTurn(turnBaseManager.coolDownTurn);
    }

}
