using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_DefeatEnemy : QuestBase
{
    public Enemy EnemyToDefeat;
    protected override void Start()
    {
        base.Start();
    }
    public override void EventHandle()
    {
        GiveQuest(this);
        this.gameObject.transform.SetParent(QuestManager.intance.transform);
    }
    private void Update() {
        if(EnemyToDefeat != null){
            if(EnemyToDefeat.isDefeated){
                CompleteQuest(this);
                Destroy(this.gameObject);
            }
        }
    }


    
}
