using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_GetInPlace : EventTRiggerBase
{
    public bool isLoopable;
       private void OnTriggerEnter(Collider other) {
        if(!isLocked){
        if(other.gameObject.GetComponent<PlayerController>()!=null){
            StartEvent();
            if(!isLoopable){
            Destroy(gameObject);
            }
        }}
        else{
            Debug.Log( gameObject.name + "is locked");
        }
    }

    public override void StartEvent()
    {
        SaveManager.intance.TriggerTriggered.Add(this.gameObject.name);
         NotifyPlayer(this.gameObject);
        if(eventStoryBases.Count >0 ){
            foreach(EventStoryBase eventStoryBase in eventStoryBases) {
        eventStoryBase.EventHandle();
        }
        }
    }

}
