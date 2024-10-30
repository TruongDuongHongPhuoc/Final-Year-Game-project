using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotifyPlayer : MonoBehaviour
{
    public static NotifyPlayer intance {get; private set;}
    [SerializeField] GameObject notifyObject;
   [SerializeField] TextMeshProUGUI textObject;
   [SerializeField] Animator animator;
   private void Awake() {
       if (intance != null && intance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            intance = this;
        }
        DontDestroyOnLoad(this.gameObject);
   }
    public void showNotify(string notifyText){
        notifyObject.SetActive(true);
        if(!notifyText.Equals("")){
            textObject.text = notifyText;
            animator.SetTrigger("notify");
        }
    }
    
}
