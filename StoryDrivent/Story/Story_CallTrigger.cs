using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story_CallTrigger: EventStoryBase
{
    [SerializeField] string triggerName;
    
    public override void EventHandle()
    {
        SaveManager.intance.eventstoriesTriggerd.Add(this.gameObject.name);
        UsingObstruction();
    }
    public void UsingObstruction()
    {
        Debug.Log(gameObject.name + " Finding" + triggerName);
        if (GameObject.Find(triggerName) != null)
        {
           GameObject trigger = GameObject.Find(triggerName);
           trigger.GetComponent<EventTRiggerBase>().StartEvent();
           StopAllCoroutines();
           Destroy(this.gameObject);
           }
           else{
            Debug.Log("No set for Behavior of Trigger in" + gameObject.name);
            StartCoroutine(StartUsing());
           }
        }
        IEnumerator StartUsing()
        {
            yield return new WaitForSeconds(0.5f);
            UsingObstruction();
        }
    }

