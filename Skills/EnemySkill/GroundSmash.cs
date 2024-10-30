using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GroundSmash : SkillBase
{
    public override void PlayEffect(GameObject enemy, GameObject self)
    {
        Vector3 direction = self.transform.position - enemy.transform.position;
        var spawned = Instantiate(this.gameObject, enemy.transform.position,Quaternion.identity);
        spawned.transform.rotation.SetLookRotation(direction);
    }
}
