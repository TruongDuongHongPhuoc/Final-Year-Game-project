using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_MagicCasster : UnitBase
{
    private void Start() {
        animator = gameObject.GetComponentInChildren<Animator>();
    }
}
