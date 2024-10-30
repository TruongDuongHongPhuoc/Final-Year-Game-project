using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tackle : SkillBase
{
    public override void PlayEffect(GameObject enemy, GameObject self)
    {
        Debug.Log("Tackle Effect");
    }
}
