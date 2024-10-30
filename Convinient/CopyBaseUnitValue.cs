using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyBaseUnitValue : MonoBehaviour
{
    public static void copyPasteValue(UnitBase copiedUnit, UnitBase unitNeedToGetValue){
         unitNeedToGetValue.UnitName = copiedUnit.UnitName;
        unitNeedToGetValue.unitElement = copiedUnit.unitElement;
        unitNeedToGetValue.CurrentHeal = copiedUnit.CurrentHeal;
        unitNeedToGetValue.MaxHeal = copiedUnit.MaxHeal;
        unitNeedToGetValue.Shield = copiedUnit.Shield;
        unitNeedToGetValue.MaxShield = copiedUnit.MaxShield;
        unitNeedToGetValue.Armor = copiedUnit.Armor;
        unitNeedToGetValue.Damage = copiedUnit.Damage;
        unitNeedToGetValue.Speed = copiedUnit.Speed;
        unitNeedToGetValue.isDeath = copiedUnit.isDeath;
        unitNeedToGetValue.Level = copiedUnit.Level;
        unitNeedToGetValue.Experience = copiedUnit.Experience;
        unitNeedToGetValue.ExperienceRequire = copiedUnit.ExperienceRequire;
        unitNeedToGetValue.currentEffect = copiedUnit.currentEffect;
    }
}
