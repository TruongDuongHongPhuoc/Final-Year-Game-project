using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_CrusaderV3 : UnitBase
{
   private void Start() {
    animator = gameObject.GetComponentInChildren<Animator>();
   }
}
