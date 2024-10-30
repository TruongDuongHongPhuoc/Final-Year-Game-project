using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
public abstract class QuestBase : EventStoryBase
{
   [HideInInspector] public GameObject UIPrefab;
     [HideInInspector] private QuestManager questManager;
    public bool isActive = false;
    public bool isNotDestroyAfterComplete;
    public string questName;
    [TextArea]
    public string questDescription;
    public List<ItemBase> Reward;
    public UnityEvent unityEvent;
    [SerializeField] GameObject AddQuestEffect;
    [SerializeField] GameObject CompleteEffect;
    public int experience;
    public int money;
    private void Awake() {
        
    }
    protected virtual void Start() {
        questManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
        UIPrefab = Resources.Load<GameObject>("UI/QuestBasePrefab");
        SetUpUIPrefab();
        if(SaveManager.intance.questDataBase.Contains(this)){
            Destroy(this);
        }else{
            Debug.Log("Add quest to database: " + this.gameObject.name + "");
        SaveManager.intance.questDataBase.Add(this);
        }
    }
    public override abstract void EventHandle();
    public virtual void GiveQuest(QuestBase quest){
        isActive = true;
        GameObject player = GameObject.Find("Player");
        Instantiate(AddQuestEffect, player.transform.position, Quaternion.identity);
        questManager.AddQuest(quest);
    }
    public virtual void CompleteQuest(QuestBase questToRemove){
        if(isActive){
        SaveManager.questCompleted.Add(questToRemove.questName);
        isActive = false;
        questManager.RemoveQuest(questToRemove);
        GivingReward();
        GameObject player = GameObject.Find("Player");
        Instantiate(CompleteEffect, player.transform.position, Quaternion.identity);
        if(!isNotDestroyAfterComplete){
        Destroy(this.gameObject);
        }
        Debug.Log("Reward Gived");
        }else{
            Debug.Log( gameObject.name + "is Active : " + isActive);
        }
    }
    public virtual void SetUpUIPrefab(){
        if(UIPrefab != null){
            UIPrefab.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = questName;
            UIPrefab.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = questDescription;
        }
    }
    public virtual void GivingReward(){
        PlayerController player = PlayerController.intance;
       Inventory playerinventory = player.inventory;
        foreach(ItemBase item in Reward){
            playerinventory.AddItem(item,1);
        }
        player.playerCondition.CurrentCharacter.useUnit.Experience += experience;
        player.playerCondition.Money += money;
        unityEvent.Invoke();
    }
    public virtual Type ReturnQuestType(){
        return this.GetType();
    }
    public void DestroySelf(){
        Destroy(this.gameObject);
    }
}
