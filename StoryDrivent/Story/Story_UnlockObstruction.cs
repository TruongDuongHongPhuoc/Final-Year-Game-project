using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story_UnlockObstruction : EventStoryBase
{
    [SerializeField] string obstructionName;
    public override void EventHandle()
    {
        SaveManager.intance.eventstoriesTriggerd.Add(this.gameObject.name);
        UsingObstruction();
    }
    public void UsingObstruction()
    {
        Debug.Log(gameObject.name + " Finding" + obstructionName);
        if (GameObject.Find(obstructionName) != null)
        {
            Obstruction g = GameObject.Find(obstructionName).GetComponent<Obstruction>();
            if (obstructionName != null && g != null)
            {
                g.UseObstruction();
                StopAllCoroutines();
                Destroy(this.gameObject);
            }
        }
        else
        {
            StartCoroutine(StartUsing());
        }
        IEnumerator StartUsing()
        {
            yield return new WaitForSeconds(0.5f);
            UsingObstruction();
        }
    }
}
