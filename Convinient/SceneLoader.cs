using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class SceneLoader
{
    public static int lastScene = 0;
    public static int currnetScene = 0;
    public static string  CurrentSceneName = "MainCity";
    public static void LoadSCene(int scene){
    currnetScene = scene;
    SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
    CheckScene();
   }
   public static void LoadSceneByName(string name){
     Debug.Log("Load Scene name " + name);
     SceneManager.LoadScene(name);
   }
   public static void LoadSceneByNameAsyn(string name){
     Debug.Log("Load Scene name " + name);
     SceneManager.LoadSceneAsync(name);
   }
   public static void unLoadSceneByNameAsyn(string name){
     Debug.Log("unLoad Scene name " + name);
     SceneManager.UnloadSceneAsync(name);
   }
   public static void LoadSceneByNameAsynAdditive(string name){
     Debug.Log("Load Scene name " + name);
     if(name.Equals(CurrentSceneName)){
        Debug.Log("There is no need to load this scene");
        return;
     }else{
          SceneManager.LoadSceneAsync(name,LoadSceneMode.Additive);
          Debug.Log("Load scene name: " + name + "Current scene name:" + currnetScene);
     }
   
   }

   public static void UnloadAsSyn(int scene){
    lastScene = currnetScene;
    SceneManager.UnloadSceneAsync(scene);
   CheckScene();
   }
   public static void CheckScene(){
        CurrentSceneName = SceneManager.GetSceneByBuildIndex(currnetScene).name; // Get the scene name by index
   }
   public SceneLoader(){
        CheckScene();
   }
   public static void LoadPlayerToOtherScene(Gate gate){
        SaveManager.intance.SaveTriggerData();
          lastScene = currnetScene;
          currnetScene = GetSceneIndexByName(gate.Destinition);
          LoadSceneByNameAsyn(gate.Destinition);
          Tele(PlayerController.intance.gameObject,gate.returnOrigin);
          UnloadAsSyn(lastScene);
   }
   public static void Tele(GameObject objectTele, Vector3 outport){
         objectTele.gameObject.GetComponent<PlayerController>().enabled = false;
        objectTele.gameObject.GetComponent<Gravity>().enabled = false;
        objectTele.transform.position = outport;
        objectTele.gameObject.GetComponent<PlayerController>().enabled = true;
        objectTele.gameObject.GetComponent<Gravity>().enabled = true;
    }
    public static int GetSceneIndexByName(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneNameFromPath = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            
            if (sceneNameFromPath == sceneName)
            {
                return i;
            }
        }
        Debug.LogError("Scene " + sceneName + " not found in build settings.");
        return -1; // Return -1 if the scene is not found
    }
    public static bool CheckSceneNameLoaded(string sceneName){
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name == sceneName)
            {
                return true;
            }
        }
        return false;
    }
     public static void UnloadAllScenesExcept(string exceptSceneName)
    {
        // Loop through all loaded scenes
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            // Check if the scene is not the one to keep
            if (scene.name != exceptSceneName)
            {
                // Unload the scene asynchronously
                Debug.Log("Unload scene name: " + scene.name);
                SceneManager.UnloadSceneAsync(scene.name);
                //  asyncUnload.completed += OnSceneUnloaded;
            }
        }
    }
    // Callback when a scene is unloaded
    private static void OnSceneUnloaded(AsyncOperation asyncOperation)
    {
        Debug.Log("Scene unloaded successfully");
    }
}
