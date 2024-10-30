using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogueFromOtherScene : MonoBehaviour
{
    public void StartDialoge(string dialogue){
        
         DialogueManager dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
         if(dialogueManager != null){
            dialogueManager.TimeLineDialogue(dialogue);
         }
    }
    public void startFaded(int time){
        if(Faded.Instance != null){
            Faded.Instance.FadedInTrue(time);
        }
    }
    public void TelePlayer(GameObject playertransform){
        if(PlayerController.intance != null)
        {
            PlayerController.intance.transform.position = playertransform.transform.position;
        }
    }
    public void startBattle(Enemy enemy){
        enemy.interact(PlayerController.intance.gameObject);
    }
}
