using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Basic: UnitBase
{
     private void Start() {
        animator = gameObject.GetComponent<Animator>();
    }
}
