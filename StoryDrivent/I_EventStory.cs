using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface I_EventStory
{
    public void EventHandle();
}
[Serializable]
public abstract class EventStoryBase : MonoBehaviour,I_EventStory
{
    public string id;
    public string sceneName;
    public bool isNotSave;
    public abstract void EventHandle();
    private void Start() {
        if(isNotSave == false){
         SaveManager sa = SaveManager.intance;
        if(!sa.eventStoryDatabase.Contains(this) || sa.eventstoriesTriggerd.Contains(this.gameObject.name)){
            
            Debug.Log("database add : " + this.gameObject.name);
            sa.eventStoryDatabase.Add(this);
        }else{
            Destroy(this);
        }
        } 
        Debug.Log("Story "+ this.gameObject.name + "is not save");
        return;
    }
}
