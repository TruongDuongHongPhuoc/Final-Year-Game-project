using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_CollectObjects : QuestBase
{
    public ItemBase itemToCollect;
    public int quantity;
    public override void EventHandle()
    {
        GiveQuest(this);
        this.gameObject.transform.SetParent(QuestManager.intance.transform);
        StartCoroutine(CheckCompleteQuest());
    }
      IEnumerator CheckCompleteQuest()
    {
        yield return new WaitForSeconds(0.5f); // Check every 0.5 seconds
        if(PlayerController.intance != null){
            int currentquantity = PlayerController.intance.inventory.ReturnAmount(itemToCollect);
            if(currentquantity >= quantity){
                CompleteQuest(this);
                Destroy(this.gameObject);
            }else{
                StartCoroutine(CheckCompleteQuest());
            }
        }

           
        }
    }


