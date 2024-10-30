using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyEffectTurn : BaseTurn
{


  public override void TurnFlow(TurnBaseManager turnBaseManager)
  {
    turnBaseManager.animator.SetTrigger("EnemyEffect");
    if (turnBaseManager.EnemyCharacter.currentEffect != null)
    {
      turnBaseManager.EnemyCharacter.EffectDeal();
      //text show damage over time
      if (turnBaseManager.EnemyCharacter.currentEffect is DamageOverTime)
      {
        string text = turnBaseManager.EnemyCharacter.UnitName + " Suffer from " + turnBaseManager.EnemyCharacter.currentEffect;
        turnBaseManager.UI.ShowDialogue(text);
      }
      else if (turnBaseManager.EnemyCharacter.currentEffect is Paralyse)
      {
        string text = turnBaseManager.EnemyCharacter.UnitName + "Being paralyse";
        turnBaseManager.UI.ShowDialogue(text);
      }
    }else{
      turnBaseManager.staticSwitchTurn(turnBaseManager.enemyInputTurn);
    }
    //switch Turn  
    turnBaseManager.SwitchTurn(turnBaseManager.enemyInputTurn);
  }

}
