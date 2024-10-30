using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Blue : SkillBase
{
    GameObject AimedEnemy;
    [SerializeField] GameObject DestroyEffect;
    public Rigidbody rigidbody;
    public bool isMoveAble;
    public float Time;
    private void Update() {
        StartMoving();
    }
    public override void PlayEffect(GameObject enemy, GameObject self)
    {
        Vector3 startPostion = self.transform.position + Vector3.forward;
        Blue skill = Instantiate(this.gameObject,startPostion, Quaternion.identity).GetComponent<Blue>();
        skill.PlayOnAwake(enemy);
    }

    IEnumerator startCouting(float t){
        isMoveAble = false;
        yield return new WaitForSeconds(t);
        isMoveAble = true;
    }
    public void PlayOnAwake(GameObject enemy){
        this.AimedEnemy = enemy;
        StartCoroutine(startCouting(Time));
    }
    public void StartMoving(){
        if(isMoveAble){
          rigidbody.velocity = AimedEnemy.transform.position - transform.position;
            isMoveAble = false;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other != null && other.gameObject == AimedEnemy) {
            Instantiate(DestroyEffect,transform.position,quaternion.identity);
            Destroy(gameObject);
        }
    }
}
