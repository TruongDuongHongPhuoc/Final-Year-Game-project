using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Quest_DefeatMultipleEnemy : QuestBase
{
     [SerializeField] List<Enemy> enemiesToDefeat;

    private bool isDefeatAll = false;

    public override void EventHandle()
    {
     
        GiveQuest(this);
        this.gameObject.transform.SetParent(QuestManager.intance.transform);
        StartCoroutine(CheckCompleteQuest());
    }

    IEnumerator CheckCompleteQuest()
    {
      enemiesToDefeat.RemoveAll(enemy => enemy == null || Object.ReferenceEquals(enemy, null));
        if(isDefeatAll){
            CompleteQuest(this);
            StopAllCoroutines();
            Destroy(this.gameObject);
        }else{
            foreach(Enemy enemy in enemiesToDefeat){
                if(enemy.isDefeated){
                    isDefeatAll = true;
                }else{
                    isDefeatAll = false;
                    break;
                }
            }
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(CheckCompleteQuest());
    }
}
