using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleave : SkillBase
{
    public override void PlayEffect(GameObject enemy, GameObject self)
    {
         Instantiate(this.gameObject,enemy.transform.position,Quaternion.identity);
    }

}
