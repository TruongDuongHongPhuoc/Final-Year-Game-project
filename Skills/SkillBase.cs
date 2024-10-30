using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
// using UnityEngine.UI;

[Serializable]
public abstract class SkillBase : MonoBehaviour
{
   //call skillName to Use skill
   public Sprite skillDisplayImage;
   public string skillName;
   public int DamageAmount;
   public Element element;
   public EffectBase effectBase;
   public int adjustBalance;
   [TextArea] public string skillDescription;
   public GameObject BattleUIPrefab;
   public string animationTrigger;
    public VideoClip clip;
    public float shakeAmplify;
    public float frequencyAmplify;
    public float shakingTime;
    public virtual void SetupPrefab(GameObject prefab, PlayerSkillUIManager manager){
         prefab.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = skillName;
      prefab.gameObject.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text =  "damage: "+ DamageAmount.ToString();
      prefab.gameObject.transform.GetChild(0).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = skillDescription;
      prefab.gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = skillDisplayImage;
      prefab.gameObject.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(()=> manager.UseSkillButton(skillName));
    }
    public virtual void CallAnimation(Animator animator , GameObject enemy, GameObject self){
      Debug.Log("call animation " + animationTrigger);
      animator.SetTrigger(animationTrigger);
      PlayEffect(enemy, self);
    }
    public abstract void PlayEffect(GameObject enemy, GameObject self);
}
