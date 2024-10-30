using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationAfter : MonoBehaviour
{
   public List<Animator> animators;
   public string trigger;
   
   public void playeranimation(){
    foreach(Animator animator in animators){
        animator.SetTrigger(trigger);
    }
   }
}
