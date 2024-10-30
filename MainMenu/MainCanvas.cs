using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    public static MainCanvas intance;
   private void Awake() {
     if(intance != null && intance != this){
            Destroy(this.gameObject);
        }else{
            intance = this;
        }
        DontDestroyOnLoad(this.gameObject);
   } 
}
