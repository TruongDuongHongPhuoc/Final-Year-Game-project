using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuga : SkillBase
{
    [SerializeField] GameObject destroyEffect;
    public GameObject AimedEnemy;
    public bool isMoveAble;
    public float time;
    private Rigidbody rd;
    public FugaArrow Arrow;
    private void Start() {
        rd = Arrow.gameObject.GetComponent<Rigidbody>();
    }
    private void Update() {
        StartMoving();
    }
   public override void PlayEffect(GameObject enemy, GameObject self)
    {
        Vector3 startPostion = self.transform.position + Vector3.up;
        Fuga skill = Instantiate(this.gameObject,startPostion, Quaternion.identity).GetComponent<Fuga>();
        skill.transform.rotation = Quaternion.LookRotation(enemy.transform.position);
        skill.PlayOnAwake(enemy);
    }

    IEnumerator startCouting(float t){
        isMoveAble = false;
        yield return new WaitForSeconds(t);
        isMoveAble = true;
    }
    public void PlayOnAwake(GameObject enemy){
        this.AimedEnemy = enemy;
        this.transform.rotation = Quaternion.LookRotation(enemy.transform.position);
        StartCoroutine(startCouting(time));
    }
    public void StartMoving(){
        if(isMoveAble){
          Arrow.MovingTowardEnemy(AimedEnemy);
            isMoveAble = false;
        }
    }
}
