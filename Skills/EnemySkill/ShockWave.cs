using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : SkillBase
{
    public override void PlayEffect(GameObject enemy, GameObject self)
    {
        Instantiate(this, self.transform.position, Quaternion.identity);
    }
}
