using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Zoltrak : SkillBase
{
   public  float time;

    [SerializeField] GameObject destroyObject;
    public override void PlayEffect(GameObject enemy, GameObject self)
    {
        Vector3 positon = self.transform.position + Vector3.forward;
             GameObject spawned = Instantiate(this.gameObject,positon,quaternion.identity).gameObject;
        spawned.transform.GetChild(0).transform.rotation.SetLookRotation(enemy.transform.position);
        spawned.gameObject.GetComponent<Zoltrak>().Desot(enemy);
    }
   IEnumerator startDesotry(GameObject enemy){
    yield return new WaitForSeconds(time);
    Instantiate(destroyObject,enemy.transform.position,quaternion.identity);
    Destroy(this.gameObject);
   }
    public void Desot(GameObject enemy){
        StartCoroutine(startDesotry(enemy));
    }
      
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
