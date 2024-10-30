using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class Npc : MonoBehaviour, I_Interact
{
    [SerializeField] Dialogue dialogue = new Dialogue();
    public Dialogue Dialogue
{
    get { return dialogue; }
    set { dialogue = value; }
}
    [SerializeField] ItemBase itemReward;
    public GameObject smallCutScene;
    public string animatorIdleState;
    public bool GiviReward;
    Animator animator;
    private void Start() {
        if(gameObject.transform.GetChild(0) != null){
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        }
        if(animator != null && !animatorIdleState.Equals("")) {
            HandleAnimationEnter(animatorIdleState);
        }
    }
    public void interact(GameObject other)
    {
        
        if(smallCutScene != null){
        smallCutScene.gameObject.SetActive(true);
        }
        DialogueManager dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        dialogueManager.StartDialogue(dialogue);
        if(itemReward != null && other != null && GiviReward){
            other.gameObject.GetComponent<PlayerController>().inventory.AddItem(itemReward,1);
            itemReward = null;
        }
        if(animator != null){
            animator.SetTrigger("interact");
        }
    }
    public void RemoveComponent(){
        Destroy(this.gameObject.GetComponent<Npc>());
    }
    public void HandleAnimationEnter(string animation){
        if(checkAnimatorParameter(animation)){
            ActiveAnimation(animation);
        }
    }
    public bool checkAnimatorParameter(string parameter){
        foreach (AnimatorControllerParameter param in animator.parameters){
            if(param.name == parameter){
                return true;
            }
        }
        return false;
    }
    public void ActiveAnimation(string animation){
        foreach (AnimatorControllerParameter param in animator.parameters){
            if(param.name == animation){
                if(param.type == AnimatorControllerParameterType.Trigger){
                    animator.SetTrigger(param.name);
                }else if(param.type == AnimatorControllerParameterType.Bool){
                    animator.SetBool(param.name, true);
                }
            }
        }
    }
    // name-sentence1-setences2-setence3
    public void ChangeSentence(string setence){
         Dialogue dialog = DialogueManager.GenerateDialogueFromString(setence);
        this.dialogue = dialog;   
    }
    public void GiviRewardToTrue(){
        GiviReward = true; 
    }
}
