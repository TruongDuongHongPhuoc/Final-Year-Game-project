using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerInputTurn : BaseTurn
{
    public string skillname;
    public TurnBaseManager turnBase;
    public UnitBase player;
    public bool isGetInput = false;
    public override void TurnFlow(TurnBaseManager turnBaseManager)
    {
        UnityEngine.Debug.Log("Turn flow player Input");
        if (turnBaseManager.PlayerCharacter.isAttackble)
        {
            turnBaseManager.animator.SetTrigger("PlayerInput");
            this.turnBase = turnBaseManager;
            this.player = turnBaseManager.PlayerCharacter;
            turnBase.UI.isGetFirstValue = false; // maybe Don't needed
            turnBase.UI.isRequireInput = true; // Maybe Don't needed
            if (isGetInput)
            {
                turnBaseManager.UI.UIcontainer.gameObject.SetActive(false);
                SkillBase skillused = turnBaseManager.PlayerCharacter.AttackEnemy(skillname, turnBaseManager.EnemyCharacter);
                if (skillused == null)
                {
                    turnBaseManager.UI.isRequireInput = true;
                    turnBase.UI.isGetFirstValue = false;
                    TurnFlow(turnBaseManager);
                    return;
                }
                //show text
                string text = turnBaseManager.PlayerCharacter.UnitName + " have Attack " + turnBaseManager.EnemyCharacter.UnitName + " With " + skillused.skillName;
                turnBaseManager.UI.ShowDialogue(text);
                turnBaseManager.continueButton.gameObject.SetActive(false);
                isGetInput = false;
                Action func = () => turnBaseManager.SwitchTurn(turnBaseManager.enemyEffectTurn);
                turnBaseManager.PlayAnimation(2f, func);
                turnBaseManager.shakeCamerainSkill(skillused);
                turnBaseManager.UI.isRequireInput = false;
                turnBaseManager.UI.isGetFirstValue = true;
            }
            else
            {
                turnBaseManager.UI.UIcontainer.gameObject.SetActive(true);
                turnBaseManager.continueButton.gameObject.SetActive(false);
                isGetInput = false;
                turnBaseManager.UI.isRequireInput = true;
                turnBaseManager.UI.isGetFirstValue = false;
            }
        }
        else
        {
            string text = turnBaseManager.PlayerCharacter.UnitName + " have been stun";
            turnBaseManager.UI.ShowDialogue(text);
            turnBaseManager.SwitchTurn(turnBaseManager.enemyEffectTurn);
        }

    }
    public void PlayerInput(string skillnameToUse)
    {
        this.skillname = skillnameToUse;
        isGetInput = true;
        if (ValidatePlayerInput(skillnameToUse))
        {
            TurnFlow(turnBase);
        }
        else
        {
            // if the input isn't valid require another input
            UnityEngine.Debug.Log("Input isn't valid " + skillnameToUse);
            turnBase.UI.isGetFirstValue = false;
            turnBase.UI.isRequireInput = true;
        }
    }
    public bool ValidatePlayerInput(string skillname)
    {
        if (skillname != null || !skillname.Equals("NULL"))
        {
            List<string> skillList = turnBase.PlayerCharacter.ReturnSkillList();
            foreach (string skill in skillList)
            {
                if (skillname.Equals(skill))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
