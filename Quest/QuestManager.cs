using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public List<QuestBase> quests;
    private Dictionary<QuestBase, GameObject> questDisplayed = new Dictionary<QuestBase, GameObject>();
    [SerializeField] private GameObject container;
   public static QuestManager intance { get; private set; }
    private void Awake() {
         if(intance != null && intance != this){
        Destroy(this.gameObject);
        }else{
            intance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    public void AddQuest(QuestBase questBase)
    {
        Debug.Log("add quest");
        if (quests.Count >= 6)
        {
            AudioManager.instance.PlayAudioHaveName("Error");
            Debug.Log("Quest Reach Limit");
            return;
        }
        else if (quests.Count < 6) { 
        Debug.Log("Add quest" + questBase.questName);
        questBase.isActive = true;
        quests.Add(questBase);
        DisplayAnQuest(questBase);
        AudioManager.instance.PlayAudioHaveName("AddQuest");
        }
        return;
    }
    public void RemoveQuest(QuestBase questBase)
    {
        quests.Remove(questBase);
        UnDisPlayAnQuest(questBase);
    }
    public void DispalyQuest()
    {
        foreach (QuestBase quest in quests)
        {
            GameObject QuestPrefab = Instantiate(quest.UIPrefab, container.transform);
            questDisplayed.Add(quest, QuestPrefab);
        }
    }
    public void DisplayAnQuest(QuestBase questBase)
    {

        GameObject QuestPrefa = Instantiate(questBase.UIPrefab, container.transform);
        QuestPrefa.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = questBase.questName;
        QuestPrefa.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = questBase.questDescription;
        questDisplayed.Add(questBase, QuestPrefa);

    }
    public void UnDisPlayAnQuest(QuestBase questToUnDisplay)
    {
        AudioManager.instance.PlayAudioHaveName("CompleteQuest");
        foreach(KeyValuePair<QuestBase,GameObject> kvp in questDisplayed){
            if(kvp.Key == questToUnDisplay){
                Debug.Log("Key Equal");
                Destroy(kvp.Value);
                questDisplayed.Remove(kvp.Key);
                break;
            }
        }
    }
    public void DefeatEnemy(Enemy enemy){
        foreach(QuestBase quest in quests){
            if(quest is Quest_DefeatSpecificTypeOfEnemy){
                quest.GetComponent<Quest_DefeatSpecificTypeOfEnemy>().defeatAnEnemy(enemy);
                return;
            }
        }
        Debug.Log("Did find any quest relate to defeat" + enemy.enemyName);
    }
    public void UnDisplayAllQuest(){
        foreach(QuestBase quest in quests){
            UnDisPlayAnQuest(quest);
        }
    }
}
