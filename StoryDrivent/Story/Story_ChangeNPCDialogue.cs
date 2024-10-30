using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story_ChangeNPCDialogue : EventStoryBase
{
    public GameObject NPCToChange;
    public Dialogue dialogueToChange;
    
   
    public override void EventHandle()
    {
      SaveManager.intance.eventstoriesTriggerd.Add(this.gameObject.name);
      if(NPCToChange != null){
        if(NPCToChange.GetComponent<Sorcerer>() != null){
            NPCToChange.GetComponent<Sorcerer>().dialogue = dialogueToChange;
        }
        if(NPCToChange.GetComponent<Npc>() != null){
            NPCToChange.GetComponent<Npc>().Dialogue = dialogueToChange;
        }
        if(NPCToChange.GetComponent<Seller>() != null){
            NPCToChange.GetComponent<Seller>().dialogue = dialogueToChange;
        }
      }else{
        Debug.LogError("GameObject is not Set Yet in Change NPC Dialogue " + gameObject.name);
      }

    }
}
