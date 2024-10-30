using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Story_RemoveObject : EventStoryBase
{
    [SerializeField] List<GameObject> objToRemove;
    public override void EventHandle()
    {
        SaveManager.intance.eventstoriesTriggerd.Add(this.gameObject.name);
        foreach (var obj in objToRemove){
            Destroy(obj);
        }
    }
}
