using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorcerer : MonoBehaviour, I_Interact
{
    public bool isActiveSkillTree;
    public Dialogue dialogue;
    [SerializeField] GameObject Camera;
    [SerializeField] Animator SkillTree;

    void Start()
    {
        Camera.gameObject.SetActive(false);
        SkillTree = GameObject.Find("SkillTree").GetComponent<Animator>();
        if (SkillTree != null){
        SkillTree.SetBool("isActive",false);
        }else{
            Debug.LogError("skill tree animation is null");
        }

    }
    public void interact(GameObject other)
    {
        if(isActiveSkillTree){
        OpenTreeSkill();
        }else{
            NormalNPC();
        }
    }
    public void OpenTreeSkill(){
        Camera.gameObject.SetActive(true);
        SkillTree.SetBool("isActive",true);
    }
    public void NormalNPC(){
        DialogueManager dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        dialogueManager.StartDialogue(dialogue);
        isActiveSkillTree = true;
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
}
