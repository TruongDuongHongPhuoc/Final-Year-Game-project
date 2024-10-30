using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTurn 
{
    public abstract void TurnFlow(TurnBaseManager turnBaseManager);
    public virtual void EnterTurn(TurnBaseManager turnBaseManager){
        turnBaseManager.currentTurn = this;
    }
}
