using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story_EnableObject : EventStoryBase
{
    public List<GameObject> ObjectToEnable;
    public override void EventHandle()
    {
        SaveManager.intance.eventstoriesTriggerd.Add(this.gameObject.name);
        foreach(GameObject obj in ObjectToEnable){
        obj.SetActive(true);
        }
    }
}
