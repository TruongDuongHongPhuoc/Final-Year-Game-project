using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class EquipmentData 
{
   public string HelmetName;
   public string ArmorName; 
   public string LowerArmorName;
   public string WeaponName;
   public string ringName;

    public EquipmentData(string helmetName, string armorName, string lowerArmorName, string weaponName, string ringName)
    {
        HelmetName = helmetName;
        ArmorName = armorName;
        LowerArmorName = lowerArmorName;
        WeaponName = weaponName;
        this.ringName = ringName;
    }
}
