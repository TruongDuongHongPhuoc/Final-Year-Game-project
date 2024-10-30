using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class InventoryData
{
  public List<string> itemID; 
  public List<int> quantities;
  public InventoryData(List<string> ids, List<int> quantity){
    this.itemID = ids;
    this.quantities = quantity;
  }
}
