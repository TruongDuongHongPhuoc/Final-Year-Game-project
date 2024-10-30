using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
public class Dismantle : SkillBase
{
    public override void PlayEffect(GameObject enemy, GameObject self)
    {
        Instantiate(this.gameObject,enemy.transform.position,quaternion.identity);
    }
}
