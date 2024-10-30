using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Story_StartTimeline : EventStoryBase
{
    public float delaytime;
    public PlayableDirector timeline;
    public override void EventHandle()
    {
        Debug.Log("active Event");
        StartCoroutine(startTimeline());
    }
    IEnumerator startTimeline(){
        yield return new WaitForSecondsRealtime(delaytime);
        timeline.gameObject.SetActive(true);
        Destroy(gameObject);
    }
}
