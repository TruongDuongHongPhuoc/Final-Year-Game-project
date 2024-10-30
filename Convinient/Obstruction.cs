using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstruction : MonoBehaviour
{
   [SerializeField]BoxCollider insideBox;
   [SerializeField] Dialogue dialogue;
   public bool isNotUse;
   private void OnTriggerEnter(Collider other) {
      if(!isNotUse){
       Debug.Log("collition with "+other.gameObject.name);
      if (other.gameObject.GetComponent<PlayerController>() != null) {
         DialogueManager dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        dialogueManager.StartDialogue(dialogue);
      }  
      }else{
         insideBox.isTrigger = true;
         Debug.Log(gameObject.name + "is not use False");
      }
   }
   public void Destroyself(){
        Destroy(gameObject);
   }
   public void UseObstruction(){
      insideBox.isTrigger = false;
      isNotUse = false;
   }
}
