using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleStatus : MonoBehaviour
{
    [SerializeField] TurnBaseManager turnBaseManager;
    [SerializeField] GameObject EnemyName;
    [SerializeField] GameObject playerName;
    [SerializeField] GameObject EnemyAvatar;
    [SerializeField] GameObject PlayerAvatar;
    [SerializeField] GameObject EnemyLevel;
    [SerializeField] GameObject playerLevel;
    [SerializeField] UnityEngine.UI.Slider EnemyHPSlider;
    [SerializeField] Slider EnemyShieldSlider;
    [SerializeField] Slider playerHPSlider;
    [SerializeField] Slider PlayerExperineceSlider;
    [SerializeField] Slider PlayerShieldSlider;
    [SerializeField] GameObject PlayerEffect;
    [SerializeField] GameObject EnemyEffect;
    [SerializeField] Image PlayerElement;
    [SerializeField] Image EnemyElement;
    [SerializeField] Sprite YingIMG;
    [SerializeField] Sprite YangImage;
    [SerializeField] Sprite Head;
    [SerializeField] Sprite Tail; 
    private void Update() {
        SetupUI();
        SetUpEffect();
    }
    public void SetupUI(){
        UnitBase playerUnit = turnBaseManager.PlayerCharacter;
        UnitBase enemyUnit = turnBaseManager.EnemyCharacter;
        SetupEnemy(enemyUnit);
        SetupPlayer(playerUnit);
    }
    public void SetupEnemy(UnitBase enemy){
        if(enemy != null){
        EnemyName.GetComponent<TextMeshProUGUI>().text = enemy.UnitName;
        EnemyLevel.GetComponent<TextMeshProUGUI>().text = "LV:"+ enemy.Level.ToString();
        EnemyHPSlider.maxValue = enemy.MaxHeal;
        EnemyHPSlider.value = enemy.CurrentHeal;
        EnemyShieldSlider.maxValue = enemy.MaxShield;
        EnemyShieldSlider.value = enemy.Shield;
        EnemyAvatar.gameObject.GetComponent<Image>().sprite = enemy.Avatar;
        EnemyElement.sprite = setupImage(enemy);
        }
    }
    public void SetupPlayer(UnitBase player){
        if(player != null){
        playerName.GetComponent<TextMeshProUGUI>().text = player.UnitName;
        playerLevel.GetComponent<TextMeshProUGUI>().text = "LV:" + player.Level.ToString();
        playerHPSlider.maxValue = player.MaxHeal;
        playerHPSlider.value = player.CurrentHeal;
        PlayerExperineceSlider.maxValue = player.ExperienceRequire;
        PlayerExperineceSlider.value = player.Experience;
        PlayerShieldSlider.maxValue = player.MaxShield;
        PlayerShieldSlider.value = player.Shield;
        PlayerAvatar.gameObject.GetComponent<Image>().sprite = player.Avatar;
        PlayerElement.sprite = setupImage(player);
        }
    }
    public void SetUpEffect(){
        if(turnBaseManager.EnemyCharacter.currentEffect == null){
            EnemyEffect.gameObject.SetActive(false);
        }else{
            EnemyEffect.GetComponent<Image>().sprite = turnBaseManager.EnemyCharacter.currentEffect.EffectDisplay;
            EnemyEffect.GetComponentInChildren<TextMeshProUGUI>().text = turnBaseManager.EnemyCharacter.currentEffect.TurnRemain.ToString();
            EnemyEffect.gameObject.SetActive(true);
        }
        if(turnBaseManager.PlayerCharacter.currentEffect == null){
            PlayerEffect.gameObject.SetActive(false);
        }else{
             PlayerEffect.GetComponent<Image>().sprite = turnBaseManager.PlayerCharacter.currentEffect.EffectDisplay;
             PlayerEffect.GetComponentInChildren<TextMeshProUGUI>().text = turnBaseManager.PlayerCharacter.currentEffect.TurnRemain.ToString();
            PlayerEffect.gameObject.SetActive(true);
        }
        
    }
    public Sprite setupImage(UnitBase unitBase){
        switch(unitBase.unitElement){
            case Element.yang:return YangImage;
            case Element.yin: return YingIMG;
            case Element.head:return Head ;
            case Element.tail: return Tail;
            default: return null;
        }
    }
}
