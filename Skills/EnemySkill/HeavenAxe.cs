using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class HeavenAxe : SkillBase
{
    public GameObject aimedEnemy;
    [SerializeField] private GameObject DestroyEffect;

    public override void PlayEffect(GameObject enemy, GameObject self)
    {
       Vector3 startPostion = self.transform.position - Vector3.forward * 2;
       var spawned = Instantiate(this.gameObject, startPostion, quaternion.identity);
       Vector3 directionToLook = enemy.transform.position - self.transform.position;
       spawned.transform.rotation = Quaternion.LookRotation(directionToLook);
       spawned.GetComponent<HeavenAxe>().aimedEnemy = enemy;
       Debug.Log("Start corotine divine axe");
        spawned.GetComponent<HeavenAxe>().StartDestroyObject();
    }
    public void StartDestroyObject(){
        StartCoroutine(playDestroyEffect());
    }
    IEnumerator playDestroyEffect(){
        yield return new WaitForSeconds(1.5f);
        Debug.Log("palyer destroy effect run ");
        Instantiate(DestroyEffect, aimedEnemy.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    
}
