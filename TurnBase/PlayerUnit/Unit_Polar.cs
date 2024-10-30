using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Polar : UnitBase
{
    // Start is called before the first frame update
    void Start()
    {
         if(animator == null)
        animator = gameObject.GetComponentInChildren<Animator>();
    }
}
