using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI DialogueText;
    [SerializeField] private GameObject DialogObject;
    private string dialogtext;
    public Queue<string> Sentences = new Queue<string>();
    public void StartDialogue(Dialogue dialogue)
    {
        Sentences.Clear();
        DialogObject.gameObject.SetActive(true);
        PlayerController.isBattling = true;
        if (dialogue.npcName != null)
        {
            Name.SetText(dialogue.npcName);
        }
        foreach (string sentence in dialogue.sentence)
        {
            Sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (Sentences.Count == 0)
        {
            EndDiaglogue();
            return;
        }
        StopAllCoroutines();
        StartCoroutine(TypeSentence(Sentences.Dequeue()));
    }
    public void EndDiaglogue()
    {
        // AudioManager.instance.StopAudioHaveName("Dialogue");
        DialogObject.gameObject.SetActive(false);
        //  Debug.Log("is move  to true");
        PlayerController.isBattling = false;
    }
    IEnumerator TypeSentence(string sentence)
    {
        // AudioManager.instance.PlayAudioHaveName("Dialogue");
        dialogtext = "";
        foreach (char letter in sentence)
        {
            dialogtext += letter;
            DialogueText.text = dialogtext;
            if(dialogtext.Equals(sentence)){
                // AudioManager.instance.StopAudioHaveName("Dialogue");
            }
            yield return null;
        }

    }
    public void ShowDialogue(string dialogtext)
    {
        string[] text = { dialogtext };
        Dialogue dialogue = new Dialogue(text);
        dialogue.npcName = "";
        this.StartDialogue(dialogue);
    }
    // dialogue : "name" space "dialogue text"
    public void TimeLineDialogue(string dialogtext)
    {
        Dialogue dialog = GenerateDialogueFromString(dialogtext);
        StartDialogue(dialog);
    }
    public static Dialogue GenerateDialogueFromString(string dialogtext){
        Dialogue dialog = new Dialogue();
        string[] splitedDialogText = dialogtext.Split('-');

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
            return new Dialogue(); // Exit the method if the format is incorrect
        }
        return dialog;
    }

}
