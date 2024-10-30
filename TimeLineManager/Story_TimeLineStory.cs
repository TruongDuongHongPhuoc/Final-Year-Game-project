using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Story_TimeLineStory : EventStoryBase
{
    public string dialogueafterTImeline;
    public int sceneIndex;
    public UnityEvent eventafterTimeLine;
    public override void EventHandle()
    {
       startScene(sceneIndex);
       Destroy(this);
    }

    public void startScene( int sceneIndex){
        eventafterTimeLine.Invoke();
        SceneLoader.LoadSCene(2);
        TimeLineToplay.SetTimeline(sceneIndex);
    }
}
public class TimeLineToplay{
    public static int Timeline;
    public static void SetTimeline(int timeline){
        Timeline = timeline;
    }
}
