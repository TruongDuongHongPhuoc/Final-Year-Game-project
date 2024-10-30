using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineParent : MonoBehaviour
{
    public int timelineIndex;
    private void Start() {
       timelineIndex = TimeLineToplay.Timeline; 
        var  g = transform.GetChild(timelineIndex).gameObject;
        g.SetActive(true);
        g.GetComponent<PlayableDirector>().enabled = true;
        Debug.Log("Active Timeline"+g.gameObject.name);
    }
    public void EndOfTimeLine(){
        SceneLoader.UnloadAsSyn(2);
         Debug.Log("is move  to true");
        PlayerController.isBattling = false;
    }
}
