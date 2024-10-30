using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public interface I_Unit
{
    public void TakingSkill(SkillBase skill, int EnemyDamage);
    public SkillBase AttackEnemy(string skillnameToUse,UnitBase unitBase);
    public void HealLing(int healAmount);
    public void EffectDeal();
}
//Enum
[Serializable]
public enum Element
{  yin,yang,head,tail}
//Abtract
[Serializable]
public abstract class UnitBase : MonoBehaviour,I_Unit{
    public string UnitName;
    public Sprite Avatar; 
    public Element unitElement;
    public int CurrentHeal; 
    public int MaxHeal;
    public int Shield; 
    public int MaxShield;
    public int Armor;
    public int Damage;
    public int Speed;
    public bool isAttackble = true; 
    public bool isDeath;
    public int Level;
    public int Experience;
    public int ExperienceRequire;
    public EffectBase currentEffect;
    public List<SkillBase> skillset;
    public Animator animator;
    public Vector3 SkillPosition;
    private void Start() {
        isAttackble = true;
    }
    public SkillBase AttackEnemy(string skillnameToUse,UnitBase enemy)
    {
        // Vector3 enemyposition = new Vector3(enemy.gameObject.transform.position.x, 0,enemy.gameObject.transform.position.z);
        // this.gameObject.transform.rotation = Quaternion.LookRotation(enemyposition);
        // Debug.Log(this.gameObject.name + "is looking at" + enemy.gameObject.name);
        SetSkillPosition();
        Debug.Log(gameObject.name + "Attack" + enemy.gameObject.name + "With" + skillnameToUse);
       if(!skillnameToUse.Equals(null) && enemy != null){
        SkillBase skillToUse = useSkill(skillnameToUse, enemy.gameObject);
        if(skillToUse == null){
            return null;
        }
        enemy.TakingSkill(skillToUse,this.Damage);
        return skillToUse;
       }else{
        Debug.LogError("Trying to use Skill that not exists or not accessible");
        return null;
       }
    }
    public virtual void TakingSkill(SkillBase skill, int EnemyDamage){
       
        if(skill == null){
            Debug.Log("UnitBase-TalkingSkill :Skill is Null");
            return;
        }
        switch(CheckSkillElement(skill)){
            case -1:
                // unit have disadvantage element talking double the damage
                TakingDamage(skill.DamageAmount, skill.DamageAmount + EnemyDamage,Color.blue);
                Debug.Log( skill.name + "Not Every Effective");
                break;
            case 0:
                // unit have neutral elemenent no additional damage 
                TakingDamage(skill.DamageAmount, EnemyDamage,Color.gray);
                 Debug.Log( skill.name + "Not So Bad"); 
                break;
            case 1: 
                // advantage unit have element taking haf of damage
                TakingDamage(skill.DamageAmount, (-skill.DamageAmount/2)+ EnemyDamage,Color.red);
                Debug.Log( skill.name + " Very Effective Deal :"); 
                break;
        }
        SetEffect(skill.effectBase);
        //animator
        if(animator == null){
            Debug.LogError("Animator null");
        }
        Debug.Log("HIt animation call");
        animator.SetTrigger("Hit");
    }
     
    public virtual void HealLing(int healAmount)
    {
        throw new NotImplementedException();
    }

    public virtual void EffectDeal()
    {
        if(currentEffect !=null){
            currentEffect.EffectDeal(this);
        }
    }

    private void TakingDamage(int damageAmount, int additionalDamage,Color popupColor){
        int totalDamage = damageAmount + additionalDamage;
         totalDamage = totalDamage - Armor;
         PopUp popUp = GameObject.Find("PopUp").GetComponent<PopUp>();
         popUp.PopUpText(totalDamage.ToString(), this.gameObject, popupColor);
         if(totalDamage <=0){
            totalDamage = 1;
         }
        //Minus Shield
        if(Shield >=0){
            Shield -= totalDamage;
            totalDamage -= Shield;
        }
        //Minus Heal
        if(totalDamage > 0){
            this.CurrentHeal -= totalDamage;
            if(CurrentHeal <=0){
                isDeath = true;
            }
        }
    }
    public void SetEffect(EffectBase effectToSet){
       if(currentEffect == null || currentEffect.Equals(null) && effectToSet != null){ 
        this.currentEffect = effectToSet;
       }
       else{
        Debug.Log("Current Unit Already Have Effect");
       }   
    }
     // 1 = win  0= neutral -1= lost
    public int CheckSkillElement(SkillBase skill){
      return CompareElement.Compare(unitElement,skill.element);    
    }
    private SkillBase useSkill(string skillnameToUse, GameObject enemy){
        if(this.animator == null){
            Debug.Log("animator" + "null");
        }
        foreach(SkillBase skills in this.skillset){
            if(skills.skillName.Equals(skillnameToUse)){
                Debug.Log("Founded" + skillnameToUse);
                if(this.animator == null){
                    Debug.LogError("Animator null in " + this.name);
                }
                skills.CallAnimation(this.animator, enemy,this.gameObject);
               return skills;
            }
        }
        Debug.Log("Do not found" + skillnameToUse);
        // return useRamdomSkill(enemy);
        return null;
    }
    private SkillBase useRamdomSkill(GameObject enemy){
      int x =UnityEngine.Random.Range(0, skillset.Count);
    skillset[x].CallAnimation(this.animator, enemy,this.gameObject);
        Debug.Log("skill set" + skillset[x].name);
      return skillset[x];
    }
    public SkillBase ramdomizeSkill(){
        int ramdom = UnityEngine.Random.Range(0,this.skillset.Count);
        
        return skillset[ramdom];
    }
    public void GainExperience(int Experience){
        this.Experience += Experience;
         if(Experience >= ExperienceRequire){
            LevelUp();
         }
    }
    public void LevelUp(){
       Debug.Log("Level Up" + Level + name);
            Experience -= ExperienceRequire;
            Level ++;
            MaxHeal += UnityEngine.Random.Range(5,10) * (Level/2);
            Damage += UnityEngine.Random.Range(3,6) * (Level/2);
             Armor += UnityEngine.Random.Range(1,2) * (Level/4);
             MaxShield += UnityEngine.Random.Range(4,6) * (Level/2);
        ExperienceRequire += Level * UnityEngine.Random.Range(10,20);
        SpawnerRpg.UpdateAllSpawner();
        GameObject levelupEffect = SaveManager.ResourceLoadFind("LevelUpEffect","LevelUp");
        Instantiate(levelupEffect,this.gameObject.transform.position,Quaternion.identity);
        NotifyPlayer.intance.showNotify(this.UnitName + "is level up to " + Level);
        AudioManager.instance.PlayAudioHaveName("LevelUp");
        PlayerController.intance.playerCondition.skillPont += 1;
    }
    public void SetSkillPosition(){
        SkillPosition = Vector3.forward;
    }
    public void PowerUpTolevel(int LevelToUp){
        if(LevelToUp <=0){
            LevelToUp =1;
        }
            MaxHeal += UnityEngine.Random.Range(5,10) * (LevelToUp/2) ;
            Damage += UnityEngine.Random.Range(3,6) * (LevelToUp/2)  ;
            Armor += UnityEngine.Random.Range(1,2) * (LevelToUp/4) ;
            MaxShield += UnityEngine.Random.Range(4,6) * (LevelToUp/2);
            this.Level = LevelToUp;
            ExperienceRequire += Level * UnityEngine.Random.Range(10,20);
            CurrentHeal = MaxHeal;
            Shield = MaxHeal;
    }
    // return skill names
    public List<string> ReturnSkillList(){
        List<string> returnList = new List<string>();
        foreach(SkillBase skill in skillset)
        {
            returnList.Add(skill.skillName);
        }
        return returnList;
    }
    // return true or false inorder to check if success equiped
    public bool EquipSkill(SkillBase skill){
        if( skillset.Count >=4){
            NotifyPlayer.intance.showNotify("you already equip 4 skills please unequip at least one to continue");
            return false;
        }
        if(skillset.Contains(skill)){
            Debug.Log("This  skill already equiped");
            NotifyPlayer.intance.showNotify("You Already equip " + skill.skillName + "already");
            return false;
        }
        if(skillset.Count <4 && !skillset.Contains(skill)){
        skillset.Add(skill); 
        NotifyPlayer.intance.showNotify("Add Skill " + skill.skillName + " success");
        return true;
        }
        return false;
    }
    public bool UnEquipSkill(SkillBase skill){
        if(skillset.Count >1 && skillset.Contains(skill)){
            skillset.Remove(skill);
            NotifyPlayer.intance.showNotify("Remove Skill" + skill.skillName +" Success");
            return true;
        }else{
            if(skillset.Count <= 1){
                NotifyPlayer.intance.showNotify("You Must remain at least 1 skill to continue");
                return false;
            }else{
                NotifyPlayer.intance.showNotify("You haven't Equip this skill Yet please Equip skill to UnEquip it");
                return false;
            }
        }
    }
}