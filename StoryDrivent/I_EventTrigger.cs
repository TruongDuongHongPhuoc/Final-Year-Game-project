using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

public interface I_EventTrigger 
{
    public void StartEvent(); 
}
[Serializable]
public abstract class EventTRiggerBase : MonoBehaviour, I_EventTrigger{
    [HideInInspector]public string id;
    public bool isTriggered;
    public GameObject notifyObject;
    public bool isLocked;
    [SerializeField] public List<EventStoryBase> eventStoryBases;
    public abstract void StartEvent();
    public virtual GameObject NotifyPlayer(GameObject posit){
        if(notifyObject != null){
            GameObject notify = Instantiate(notifyObject, new Vector3(0,0,0), Quaternion.identity,transform);
            notify.transform.SetParent(posit.transform);
            notify.transform.localPosition = new Vector3(0,2,0);
       return notify;
        }else{
            return null;
        }
    }
   private void Start() {
    if(!isLocked){
        this.id = this.gameObject.name;
        if(SaveManager.intance.TriggerTriggered.Contains(id)){
            Destroy(this.gameObject);
        }else{
        SaveManager.intance.eventTriggerDataBase.Add(this);
        }
   }else{
    Debug.Log("trigger " + gameObject.name + "is locked");
   }
   }
}
