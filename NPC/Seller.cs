using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seller : MonoBehaviour, I_Interact
{
    public Dialogue dialogue;
    public bool isOpenShop;
    private DisplayStore displayStore;
    [SerializeField] Inventory Store;
    [SerializeField] private Animator animator;
    public string animationstate;
    private void Start() {
        displayStore = GameObject.Find("StoreDisplay").GetComponent<DisplayStore>();
        animator = gameObject.GetComponentInChildren<Animator>();
        ActiveAnimation(animationstate);
    }
    public void interact(GameObject other)
    {
        if(isOpenShop){
        displayStore.currentStore = Store;
        displayStore.DisplayTheStore();
       displayStore.transform.GetChild(0).gameObject.SetActive(true);
        }else{
            Talking();
        }
    }
    public void Talking(){
          DialogueManager dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        dialogueManager.StartDialogue(dialogue);
        isOpenShop = true;
    }
    public void ChangeSentence(string setence){
         Dialogue dialog = new Dialogue();
        string[] splitedDialogText = setence.Split('-');

        if (splitedDialogText.Length > 1)
        {
            dialog.npcName = splitedDialogText[0].Trim(); // Trim to remove extra spaces

            // Create a string array to store the sentences
            string[] sentences = new string[splitedDialogText.Length - 1];

            // Loop through the split parts starting from index 1
            for (int i = 1; i < splitedDialogText.Length; i++)
            {
                sentences[i - 1] = splitedDialogText[i].Trim(); // Trim each sentence to remove extra spaces
            }

            dialog.sentence = sentences;
        }
        else
        {
            Debug.LogError("Wrong format of input");
            return; // Exit the method if the format is incorrect
        }
        this.dialogue = dialog;   
    }
    public void IsOpenShopFalse(){
        isOpenShop = false;
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
}
