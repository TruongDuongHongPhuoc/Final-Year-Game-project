using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Helmet Object", menuName ="Inventory system/Item/Equipment/HandHoldWeapon")]
public class HandHoldWeapon : ItemBase
{
    public int damage;
    public int speed;
    public override void UseOfItem()
    {
         PlayerController.intance.EquipItemToCharacter(this);
    }
     public override Dictionary<string, int> ReturnPropertise()
    {
         Dictionary<string,int> returnDic = new Dictionary<string, int>
        {
            { "Damage: ", damage },
            { "Speed: ", speed }
        };
        return returnDic;
    }
}
