using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// comsumable -> uesable  / equipment -> equipable / basic -> useless
public enum ItemType{Comsumable, Equipment,Basic, All}
public abstract class ItemBase : ScriptableObject
{
   [SerializeField] public GameObject UIprefab;
   [SerializeField] public Sprite displayImg;
    [SerializeField] public ItemType ItemType;
    [TextArea]
    public string description;
    public int price;
    public int SellPrice;
    public abstract void UseOfItem();
    public virtual Dictionary<string,int> ReturnPropertise(){
        return null;
    }
}
