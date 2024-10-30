using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
  public GameObject PopUpPrefab;
  public void PopUpText(string text,GameObject position, Color color){
        TextMesh po = PopUpPrefab.GetComponentInChildren<TextMesh>();
        po.color = color;
        po.text = text;
        Instantiate(PopUpPrefab, position.transform.position, Quaternion.identity, transform);
    }   
}
