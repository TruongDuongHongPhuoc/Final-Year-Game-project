using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportal : MonoBehaviour, I_Interact
{
    [SerializeField] public Faded faded;
    public GameObject outPort;
    public void Tele(GameObject objectTele){
        SceneLoader.Tele(objectTele,outPort.transform.position);
    }

    public void interact(GameObject other)
    {
        Debug.Log("Tele");
        if(other != null && other.GetComponent<PlayerController>() != null){
            faded.FadedInTrue(3f);
            StartCoroutine(StartTele(other));
        }
    }
    IEnumerator StartTele(GameObject tele){
        yield return new WaitForSeconds(2f);
        Tele(tele);
    }
   
}
