using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Video;
public class FirstMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject firstCutscene;
    [SerializeField] VideoPlayer videoPlayer;
    public bool isOpen;
    private void Start() {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
            if(isOpen){
               PlayerController.isBattling = true;
            }
        }
    }

    private void OnVideoEnd(VideoPlayer source)
    {
        setOpenFalse();
    }

    public void NewGameButton(){
        mainMenu.SetActive(false);
        firstCutscene.SetActive(true);
         Debug.Log("is move  to true");
        PlayerController.isBattling = false;
   }
   public void LoadGameButton(){
        MainMenu.Instance.ActiveSaveMenu();
   }
   public void InstructionButton(){
        Debug.Log("I need instruction");
   }
   public void SettingButton(){
        Debug.Log("I need Setting");
   }
   public void ExitButton(){
        Application.Quit();
   }
   public void setOpenFalse(){
        isOpen = false;
        Destroy(firstCutscene);
        mainMenu.SetActive(false);
   }
   public void OpenFirstMenu(){
        isOpen = true;
        mainMenu.SetActive(true);
   }
   
}
