using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest_interactFromOtherScene : QuestBase
{
    [SerializeField] string npcName;
    private GameObject objectToInteract;
    public bool Notify;
    private bool isAdded;
    public GameObject notifyObject;
    private GameObject annouceObject;
    public override void EventHandle()
    {
        if( isAdded == false ){
        GiveQuest(this);
        StartCoroutine(startFindObject());
        }
        this.gameObject.transform.SetParent(QuestManager.intance.transform);
    }
    private void Update() {
        if(objectToInteract != null && PlayerController.InteractedGameObject != null){
        Debug.Log("object to inteact" + objectToInteract);
        if(PlayerController.InteractedGameObject.name.Equals(npcName)){
            Destroy(annouceObject); 
            CompleteQuest(this);
        }}
    }
    IEnumerator startFindObject(){
        Debug.Log("Find interact Object....");
        if(objectToInteract == null){
            objectToInteract = GameObject.Find(npcName);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(startFindObject());
        }else{
            annouceObject = Instantiate(notifyObject, objectToInteract.transform);
            StopAllCoroutines();
        }
    }
}
