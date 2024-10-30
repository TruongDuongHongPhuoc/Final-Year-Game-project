using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class TriggerData
{
    [SerializeField] public List<string> ids;

    public TriggerData( List<string> id)
    {
        this.ids = id;
    }
}
