using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusader : UnitBase
{
    private void Start() {
        animator = gameObject.GetComponentInChildren<Animator>();
    }
  
}
