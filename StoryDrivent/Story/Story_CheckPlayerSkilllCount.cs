using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnlockFirstSKillCheck : EventStoryBase
{
    public Dialogue dialogue;
    public GameObject obstruction;
    public override void EventHandle()
    {
        SaveManager.intance.eventstoriesTriggerd.Add(this.gameObject.name);
        if(PlayerController.intance.playerCondition.CurrentCharacter.useUnit.skillset.Count >0){
            Destroy(obstruction);
        }else{
              DialogueManager dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        dialogueManager.StartDialogue(dialogue);
        }
    }
}
