using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostTurn : BaseTurn
{
  public override void TurnFlow(TurnBaseManager turnBaseManager)
  {
    turnBaseManager.animator.SetTrigger("Lost");
    turnBaseManager.PlayerCharacter.animator.SetTrigger("Death");
    //text show
    string text = "you have lose the battle return when you stronger";
    turnBaseManager.UI.ShowDialogue(text);
  }
}


