using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidObject : MonoBehaviour, I_Interact
{
    public Dialogue dialogue; 
    public void interact(GameObject other)
    {
        DialogueManager dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        dialogueManager.StartDialogue(dialogue);
    }
}
