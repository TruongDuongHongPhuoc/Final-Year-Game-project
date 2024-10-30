using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ManevolentShrine : SkillBase
{
    public float t;
    [SerializeField] GameObject DestroyObject;
    public override void PlayEffect(GameObject enemy, GameObject self)
    {
        Vector3 position = self.transform.position + Vector3.forward + Vector3.forward + Vector3.up*3;
       var spawned = Instantiate(this.gameObject, position,quaternion.identity);
        spawned.gameObject.GetComponent<ManevolentShrine>().startDestroy();
    }
    public void startDestroy(){
        StartCoroutine(CountingDestroy());
    }
    IEnumerator CountingDestroy(){
        yield return new WaitForSeconds(t);
        Instantiate(DestroyObject,transform.position,quaternion.identity);
        Destroy(this.gameObject);
    }

    
}
