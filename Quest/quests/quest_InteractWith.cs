using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest_InteractWith : QuestBase
{
    [SerializeField] GameObject objectToInteract;
    public bool Notify;
    public GameObject notifyObject;
    private GameObject annouceObject;
    public override void EventHandle()
    {
        GiveQuest(this);
        if(notifyObject){
        Instantiate(notifyObject, objectToInteract.transform.position,Quaternion.identity);
        }
        this.gameObject.transform.SetParent(QuestManager.intance.transform);
    }
    private void Update() {
        if(PlayerController.InteractedGameObject != null){
        if(PlayerController.InteractedGameObject == objectToInteract || PlayerController.InteractedGameObject.Equals(objectToInteract)){
            Destroy(annouceObject); 
            CompleteQuest(this);
            
        }}
    }
}
