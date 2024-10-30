using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Interact : EventTRiggerBase
{
    [SerializeField] private GameObject requireGameobject;
    GameObject notify;
    public bool isNotify;
    [SerializeField]private bool isStarted;
      public override void StartEvent()
    {
        if(!isLocked){
        SaveManager.intance.TriggerTriggered.Add(this.gameObject.name);
        Destroy(notify);
        if (eventStoryBases.Count > 0)
        {
            isStarted = true;
            foreach (EventStoryBase eventStoryBase in eventStoryBases)
            {
                eventStoryBase.EventHandle();
            }
        }}else{
            Debug.Log(gameObject.name + "is not able to trigger");
        }
    }

    private void Update()
    {
        if (PlayerController.InteractedGameObject == requireGameobject)
        {
            if (eventStoryBases.Count > 0 && !isStarted && !isLocked)
            {
                StartEvent();
                Destroy(this.gameObject);
            }
        }
    }
    private void OnEnable()
    {
        this.id = this.gameObject.name;
        if (isNotify && !isLocked)
        {
            notify = NotifyPlayer(requireGameobject);
        }
    }
    public GameObject ReturnRequireGameObject(){
        return requireGameobject;    }
    public void OnDestroy(){
        // Debug.Log(gameObject.name + "is Destroyed for NO REASON");
    }
}


