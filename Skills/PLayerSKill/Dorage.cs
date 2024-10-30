using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Dorage : SkillBase
{
    public GameObject destroyEffect;
    public override void PlayEffect(GameObject enemy, GameObject self)
    {
        Vector3 direction = self.transform.position + Vector3.up;
        var spawned = Instantiate(this,direction, quaternion.identity);
        Instantiate(this.destroyEffect, enemy.transform.position, quaternion.identity);
        spawned.transform.rotation = Quaternion.LookRotation(enemy.transform.position);
    }
}
