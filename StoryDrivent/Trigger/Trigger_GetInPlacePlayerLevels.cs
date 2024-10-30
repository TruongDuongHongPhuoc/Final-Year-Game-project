using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_PlayerLevels : EventTRiggerBase
{
    public int levelRequire;
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
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.GetComponent<PlayerController>() != null & PlayerController.intance.playerCondition.CurrentCharacter.useUnit.Level >= levelRequire){
            StartEvent();
            Destroy(this.gameObject);
        }
    }
}
