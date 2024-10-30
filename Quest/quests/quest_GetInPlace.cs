using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest_GetInPlace : QuestBase
{
    
    public override void EventHandle()
    {
        GiveQuest(this);
        this.gameObject.transform.SetParent(QuestManager.intance.transform);
    }
    private void OnTriggerEnter(Collider other) {
        if(other!= null){
            Debug.Log("interact with get in places " + other.gameObject.name);
            if(other.gameObject.GetComponent<PlayerController>()!=null){
                Debug.Log("Have player Controller");
                 CompleteQuest(this);
            }else{
                Debug.Log("Do not have playerController Component");
            }
        }
    }
}
