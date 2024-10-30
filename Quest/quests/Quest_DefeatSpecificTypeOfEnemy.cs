using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_DefeatSpecificTypeOfEnemy : QuestBase
{
    [SerializeField] Enemy enemeNeedToDefeat;
    public float quantity;
    public float currentQuantity;
    public override void EventHandle()
    {
        GiveQuest(this);
        this.gameObject.transform.SetParent(QuestManager.intance.transform);
    }
    public void defeatAnEnemy(Enemy enemy){
        if(enemy.enemyName.Equals(enemeNeedToDefeat.enemyName)){
        currentQuantity += 1; 
        if(currentQuantity == quantity){
            CompleteQuest(this);
            Debug.Log("Complete "+ gameObject.name);
        }
        }else{
            Debug.Log("Wrong type of enemy needed: " + enemy.enemyName + " - "+ enemeNeedToDefeat.enemyName);
        }

    }
}
