using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story_ChangePosition : EventStoryBase
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] GameObject objectposition;
    public override void EventHandle()
    {
        
        objectToSpawn.transform.position = objectposition.transform.position;
    }
}
