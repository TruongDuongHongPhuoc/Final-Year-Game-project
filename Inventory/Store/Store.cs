using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "New Store", menuName = "Inventory system/ New Store")]
public class Store : ScriptableObject
{
    [SerializeField]public List<GameObject> sellItems;// prefab 
}
