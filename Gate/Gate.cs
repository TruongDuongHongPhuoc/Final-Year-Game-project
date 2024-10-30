using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Gate : MonoBehaviour, I_Interact
{
    public string Destinition;
    public Vector3 returnOrigin;
    public void interact(GameObject other)
    {
        Faded.Instance.FadedInTrue(3f);
       StartCoroutine(startTele());
    //    SceneLoader.UnloadAsSyn(SceneLoader.lastScene);
    }
    IEnumerator startTele(){
        yield return new WaitForSeconds(2f);
         SceneLoader.LoadPlayerToOtherScene(this);
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject == PlayerController.intance.gameObject){
            interact(other.gameObject);
        }
    }
}
