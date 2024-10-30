using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class QuestData 
{
    // public Dictionary<string,string> Activedquestnames;
    public List<string> questActivatedNames;
    public List<string> sceneRequireToLoad;
    public List<string> CompletedQuestNames;

    public QuestData(List<string> sceneToLoad,List<string> activedquestnames, List<string> completedQuestNames)
    {
        sceneRequireToLoad = sceneToLoad;
        questActivatedNames = activedquestnames;
        CompletedQuestNames = completedQuestNames;
    }
}
