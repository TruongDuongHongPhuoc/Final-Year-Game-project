using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class SkillBlock : MonoBehaviour
{
    public string skillBlockId;
    public SkillBase skill;
    public bool isUnlock;
    public bool isUnlockAble; 
    public int skillPointRequire;
    [HideInInspector]
    public UnitBase character;
    public List<SkillBlock> requireBlock;
    [SerializeField] List<GameObject> UnlockRoad;
    private void Start() {
        
    }
     public bool ReturnUnlockStatus(){
        return isUnlock;
    }
    public void SetUnlockAbleToTrue(){
        isUnlockAble = true;
       
    }
    public void SetUnlockAbleToFalse(){
        isUnlockAble = false;
    }
    public void StaticUnLockSkill(){
        isUnlockAble = false;
        LightenTheRoad();
        SkillsManager.Instance.CreateUIObject(skill);
    }
    public void UseSkillPointToUnlock(){
        CheckRequireBlock();
        if(isUnlockAble && CheckAvailableSkillPoint() >= skillPointRequire && !isUnlock){
        PlayerController.intance.playerCondition.UseSkillPoint(skillPointRequire);
        this.skillPointRequire = 0;
        if(skillPointRequire <=0){
            isUnlock = true;
             Debug.Log("Success unlock" + skill);
             NotifyPlayer.intance.showNotify("your have successfully unlock: " + skill.skillName);
             AudioManager.instance.PlayAudioHaveName("Success");
             LightenTheRoad();
            //  character.skillset.Add(skill);
             isUnlockAble = false;
            SkillsManager.Instance.CreateUIObject(skill);
            SaveManager.intance.skillUnlocked.Add(this.skillBlockId);
        }}
        else{
            AudioManager.instance.PlayAudioHaveName("Error");
            NotifyPlayer.intance.showNotify("your have avaiable to unlock this skill yet please ensure have enough point and unlock under(s) skill first");
            Debug.Log("Please unlock Requirement Skills First or amount of skill point avaiable is not enough");
            Debug.Log("Current skill point " + PlayerController.intance.playerCondition.skillPont);
        }
    }
    public int CheckAvailableSkillPoint(){
        return PlayerController.intance.playerCondition.skillPont;
    }
    public void CheckRequireBlock(){
        if(requireBlock.Count > 0){
        foreach(SkillBlock block in requireBlock){
            if(block.isUnlock){
                SetUnlockAbleToTrue();
            }else{
                SetUnlockAbleToFalse();
                break;
            }
        }}else{
            SetUnlockAbleToTrue();
        }
    }
    public void LightenTheRoad(){
        Debug.Log("Skill is Ligten Road to it " + gameObject.name);
        foreach(GameObject objectRenderer in UnlockRoad){
            // Ensure the material has an emission property
            StartCoroutine(ChangeColorRoad(objectRenderer));
        }
    }
    IEnumerator ChangeColorRoad(GameObject objectRenderer){
        yield return new WaitForSeconds(0.1f);
         Color color= Color.white;
         objectRenderer.GetComponent<UnityEngine.UI.Image>().color = color;
    }
}
