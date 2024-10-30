using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New BasicItem", menuName ="Inventory system/Item/BasicItem")]
public class BasicItem : ItemBase
{
    public override void UseOfItem()
    {
        Debug.Log("Cannot use");
    }
}
