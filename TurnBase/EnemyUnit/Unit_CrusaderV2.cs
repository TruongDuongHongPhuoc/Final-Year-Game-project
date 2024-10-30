using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unit_CrusaderV2 : UnitBase
{
   private void Start() {
    animator = gameObject.GetComponent<Animator>();
   }
}
