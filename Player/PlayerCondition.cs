using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using UnityEngine;
[Serializable]
public class PlayerCondition 
{
   public int Money;
   public int skillPont;
   [SerializeField] public GameObject CurrentSkin;
   [SerializeField] public CharacterBase CurrentCharacter;
   [SerializeField]List<TextMeshProUGUI> textstatus;
   public Helmet helmet;
   public Armor Chest;
   public LowerArmor pant;
   public HandHoldWeapon Weapon;
   public Ring Ring;
   public void UseSkillPoint(int skillPointRequire){
      skillPont -= skillPointRequire;
   }
   public void EquipItems(ItemBase itemBase){
      if (itemBase == null || itemBase.ItemType != ItemType.Equipment) return;
      if(itemBase is Helmet && helmet ==null){
         this.helmet = itemBase as Helmet;
         CurrentCharacter.useUnit.MaxHeal += helmet.HP;
         CurrentCharacter.useUnit.Damage += helmet.damage;
         CurrentCharacter.useUnit.Armor += helmet.additionalArmor;
      }
      if(itemBase is Armor && Chest == null){
         this.Chest = itemBase as Armor;
          CurrentCharacter.useUnit.MaxHeal += Chest.HP;
         CurrentCharacter.useUnit.Damage += Chest.damage;
         CurrentCharacter.useUnit.Armor += Chest.additionalArmor;
      }
       if(itemBase is LowerArmor && pant == null){
         this.pant = itemBase as LowerArmor;
         CurrentCharacter.useUnit.MaxHeal += pant.HP;
         CurrentCharacter.useUnit.Damage += pant.damage;
         CurrentCharacter.useUnit.Armor += pant.additionalArmor;
      }
       if(itemBase is HandHoldWeapon){
         this.Weapon = itemBase as HandHoldWeapon;
         CurrentCharacter.useUnit.Damage += Weapon.damage;
         CurrentCharacter.useUnit.Speed += Weapon.speed;
      }
      if(itemBase is Ring && Ring == null){
         this.Ring = itemBase as Ring;
         CurrentCharacter.useUnit.Damage += Ring.damage;
         CurrentCharacter.useUnit.Armor += Ring.additionalArmor;
         CurrentCharacter.useUnit.MaxHeal += Ring.HP;
      }
   }
   public void UnEquipItem(ItemBase itemBase){
      if (itemBase == null || itemBase.ItemType != ItemType.Equipment) return;
      if(itemBase is Helmet && itemBase == helmet){
         this.helmet = itemBase as Helmet;
         CurrentCharacter.useUnit.MaxHeal -= helmet.HP;
         CurrentCharacter.useUnit.Damage -= helmet.damage;
         CurrentCharacter.useUnit.Armor -= helmet.additionalArmor;
         helmet =null;
      }
      if(itemBase is Armor){
         this.Chest = itemBase as Armor;
          CurrentCharacter.useUnit.MaxHeal -= Chest.HP;
         CurrentCharacter.useUnit.Damage -= Chest.damage;
         CurrentCharacter.useUnit.Armor -= Chest.additionalArmor;
         Chest = null;
      }
       if(itemBase is LowerArmor){
         this.pant = itemBase as LowerArmor;
         CurrentCharacter.useUnit.MaxHeal -= pant.HP;
         CurrentCharacter.useUnit.Damage -= pant.damage;
         CurrentCharacter.useUnit.Armor -= pant.additionalArmor;
         pant = null;
      }
       if(itemBase is HandHoldWeapon){
         this.Weapon = itemBase as HandHoldWeapon;
         CurrentCharacter.useUnit.Damage -= Weapon.damage;
         CurrentCharacter.useUnit.Speed -= Weapon.speed;
         Weapon = null;
      }
      if(itemBase is Ring){
         this.Ring = itemBase as Ring;
         CurrentCharacter.useUnit.Damage -= Ring.damage;
         CurrentCharacter.useUnit.Armor -= Ring.additionalArmor;
         CurrentCharacter.useUnit.MaxHeal -= Ring.HP;
         Ring = null;
      }
   }
   
}
