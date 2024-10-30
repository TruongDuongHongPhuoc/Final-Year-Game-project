using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[Serializable]
public abstract class EffectBase : ScriptableObject
{
    public int TurnRemain;
    public int TurnStay; 
    public string effectName;
    public Sprite EffectDisplay;
    public abstract void EffectDeal(UnitBase unit);
    public virtual void TurnRemainDecrease(UnitBase unit){
        TurnRemain -= 1;
        if(TurnRemain <=0){
            unit.currentEffect = null;
            TurnRemain = TurnStay;  
        }
    }
}
