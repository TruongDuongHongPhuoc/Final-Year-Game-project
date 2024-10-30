using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float timeRemain;
    private void Update() {
        timeRemain -= Time.deltaTime;
        if(timeRemain < 0){
            Destroy(this.gameObject);
        }
    }
}
