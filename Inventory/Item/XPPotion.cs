using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Postion Object", menuName ="Inventory system/Item/XP Potion")]
public class XPPotion : ItemBase
{
    public int Xp;
    public override void UseOfItem()
    {
        PlayerController.intance.playerCondition.CurrentCharacter.useUnit.GainExperience(Xp);
    }
}
