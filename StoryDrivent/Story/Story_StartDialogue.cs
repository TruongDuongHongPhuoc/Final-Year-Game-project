using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story_StartDialogue : EventStoryBase
{
    public Dialogue dialogue;
    public override void EventHandle()
    {
        DialogueManager dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        dialogueManager.StartDialogue(dialogue);
    }
}   
