using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Trigger_DefeatEnemy : EventTRiggerBase
{
    [SerializeField] Enemy enemyToDefeat;
    public override void StartEvent()
    {
        SaveManager.intance.TriggerTriggered.Add(this.gameObject.name);
        foreach(EventStoryBase enven in eventStoryBases){
         enven.EventHandle();
        }
    }
    private void Update() {
        if(enemyToDefeat.isDefeated){
            StartEvent();
            Destroy(this.gameObject);
        }
    }

}
