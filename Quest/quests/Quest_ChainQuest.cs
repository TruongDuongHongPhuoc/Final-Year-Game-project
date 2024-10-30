using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_ChainQuest : QuestBase
{
    public override void EventHandle()
    {
        QuestBase firstquest = gameObject.transform.GetChild(0).gameObject.GetComponent<QuestBase>();
        if(firstquest != null){
        firstquest.EventHandle();
        }
        this.gameObject.transform.SetParent(QuestManager.intance.transform);
    }
    public void CompleteChainQuest(){
        unityEvent.Invoke();
        Destroy(this.gameObject);
    }
}
