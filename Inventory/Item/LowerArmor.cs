using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Helmet Object", menuName ="Inventory system/Item/Equipment/LowerArmor")]
public class LowerArmor : ItemBase
{
    public int damage;
    public int HP;
    public int additionalArmor;
    public GameObject equipPrefab;
    public override void UseOfItem()
    {
        PlayerController.intance.EquipItemToCharacter(this);
    }
     public override Dictionary<string, int> ReturnPropertise()
    {
         Dictionary<string,int> returnDic = new Dictionary<string, int>
        {
            { "Damage: ", damage },
            { "HP: ", HP },
            { "Armor: ", additionalArmor }
        };
        return returnDic;
    }
}
