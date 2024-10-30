using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAtPlayerRotationLookToEnemy : SkillBase
{
   public override void PlayEffect(GameObject enemy, GameObject self){
    var spawned =    Instantiate(this, self.transform.position,Quaternion.identity);
        spawned.transform.rotation = Quaternion.LookRotation(enemy.transform.position);
   }
}
