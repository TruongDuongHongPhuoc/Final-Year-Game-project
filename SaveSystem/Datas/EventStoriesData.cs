using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class EventStoriesData
{
    public List<string> eventStoriesActivated;

    public EventStoriesData(List<string> eventStoriesActivated)
    {
        this.eventStoriesActivated = eventStoriesActivated;
    }
}
