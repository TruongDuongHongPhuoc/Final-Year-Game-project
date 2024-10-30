using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TresureOfDemiGod : SkillBase
{
    public GameObject destroyEffect;
    public override void PlayEffect(GameObject enemy, GameObject self)
    {
        Vector3 direction = self.transform.position + Vector3.up;
        var spawned = Instantiate(this, direction, Quaternion.identity);
        spawned.transform.rotation = Quaternion.LookRotation(enemy.transform.position);
        if(destroyEffect != null){
        Instantiate(destroyEffect, enemy.transform.position, Quaternion.identity);
        }
    }
}
