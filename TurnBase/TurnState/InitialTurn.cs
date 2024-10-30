using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialTurn : BaseTurn
{
  public BaseTurn turnToSwitch;
  public override void TurnFlow(TurnBaseManager turnBaseManager)
  {
    turnToSwitch = turnBaseManager.playerEffectTurn;
    if (turnBaseManager.EnemyCharacter == null || turnBaseManager.EnemyCharacter.CurrentHeal <= 0)
    {
      turnBaseManager.EnemyCharacter = turnBaseManager.EnemyRemain.Dequeue();
      turnBaseManager.EnemyCharacter = turnBaseManager.Spawn(turnBaseManager.EnemyCharacter, turnBaseManager.enemyPosition).GetComponent<UnitBase>();
      Debug.Log("Enemy remain" + turnBaseManager.EnemyRemain.Count);
      LookAtEachChOtherPosition(turnBaseManager.PlayerCharacter.gameObject, turnBaseManager.EnemyCharacter.gameObject);
    }

    // show text
    turnBaseManager.UI.ShowDialogue("an " + turnBaseManager.EnemyCharacter.UnitName + " Have Appear");
    //switch turn
    if (isPlayerMoveFirst(turnBaseManager.PlayerCharacter, turnBaseManager.EnemyCharacter))
    {
      turnToSwitch = turnBaseManager.playerEffectTurn;
    }
    else
    {
      turnToSwitch = turnBaseManager.enemyEffectTurn;
    }
    Debug.Log(turnToSwitch);

    turnBaseManager.SwitchTurn(turnToSwitch);
  }
  public void LookAtEachChOtherPosition(GameObject player, GameObject enemy)
  {
    Vector3 EnemyLookdirection = player.transform.position - enemy.transform.position;
    Vector3 PlayerLookdirection = enemy.transform.position - player.transform.position;

    EnemyLookdirection.y = 0;
    PlayerLookdirection.y = 0;

    player.transform.rotation = Quaternion.LookRotation(PlayerLookdirection);
    enemy.transform.rotation = Quaternion.LookRotation(EnemyLookdirection);
  }
  public bool isPlayerMoveFirst(UnitBase playerCharacter, UnitBase enemyCharacter)
  {
    if (playerCharacter.Speed >= enemyCharacter.Speed)
    {
      return true;
    }
    return false;

  }
}
