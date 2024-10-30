using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkStar : SkillBase
{
    public override void PlayEffect(GameObject enemy, GameObject self)
    {
        Instantiate(this, enemy.transform.position, Quaternion.identity);
    }
}
