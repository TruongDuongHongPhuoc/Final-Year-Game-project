using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_NifeThief : UnitBase
{
   private void Start() {
        animator = gameObject.GetComponentInChildren<Animator>();
    }
}
