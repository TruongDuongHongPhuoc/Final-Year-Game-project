using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBalll : SkillBase
{
    public override void PlayEffect(GameObject enemy, GameObject self)
    {
         Debug.Log("Fire Ball Effect");
    }

    private void Awake() {
        this.animationTrigger = "CastMagic";
    }
}
