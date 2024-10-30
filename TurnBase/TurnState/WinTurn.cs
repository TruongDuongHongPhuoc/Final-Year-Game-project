using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WinTurn : BaseTurn
{
    public override void TurnFlow(TurnBaseManager turnBaseManager)
    {
        turnBaseManager.UI.ShowDialogue("Player Win");
        turnBaseManager.animator.SetTrigger("Win");
        if(turnBaseManager.EnemyRemain.Count <=0){
            turnBaseManager.EnemyDefeat();
             Debug.Log("is move  to true");
            // PlayerController.isMoveAble = true;
            PlayerController.isBattling = false;
            Sender.isWin = true;
            Sender.enemyBattling.Defeated();
            CopyBaseUnitValue.copyPasteValue(turnBaseManager.PlayerCharacter,PlayerController.intance.playerCondition.CurrentCharacter.useUnit);
            ShowWinScreen(turnBaseManager.winscreen);
            Action func = () => SceneLoader.unLoadSceneByNameAsyn("Battle_City");
            turnBaseManager.PlayAnimation(5f, func);
            AudioManager.instance.PlayThemeMusic("Theme");
            // SceneLoader.UnloadAsSyn(SceneLoader.currnetScene);
        }else{
            turnBaseManager.EnemyDefeat();
            Action func = () => turnBaseManager.SwitchTurn(turnBaseManager.initialTurn);
            turnBaseManager.PlayAnimation(2f, func);
        }
    }
    public void ShowWinScreen(WinScreenDisplayer winScreen){
        winScreen.gameObject.SetActive(true);
        winScreen.DisplayWinSceen();
    }
}
