using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story_UnclockStory : EventStoryBase
{
    public string objectName;
   
    public override void EventHandle()
    {
        SaveManager.intance.eventstoriesTriggerd.Add(this.gameObject.name);
        enableObject();
    }
    // Start is called before the first frame update
    public void enableObject()
    {
        Debug.Log("Finding " + objectName);
        GameObject f = GameObject.Find(objectName);
        if ( f != null)
        {
              Debug.Log("Finding out" + objectName);
            f.GetComponent<EventStoryBase>().EventHandle();
            StopAllCoroutines();
            return;
        }else{
            Debug.Log("do not find out any gameobject name: " + objectName);
            StartCoroutine(startFinding());
        }
    }
    IEnumerator startFinding(){
        yield return new WaitForSeconds(0.5f);
        enableObject();
    }
}
