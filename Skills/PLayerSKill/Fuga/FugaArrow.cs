using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FugaArrow : MonoBehaviour
{
    Rigidbody rd;
    GameObject CollideEnemy;
    [SerializeField] float AdjustZAxist;
    [SerializeField] GameObject destroyEffect;
    private void Start()
    {
        rd = gameObject.GetComponent<Rigidbody>();
    }
    public void MovingTowardEnemy(GameObject aimedEnemy)
    {
        if (aimedEnemy != null)
        {
            CollideEnemy = aimedEnemy;
            Vector3 direction = aimedEnemy.transform.position - this.gameObject.transform.position;
            rd.velocity = direction.normalized * 15;
            direction.z = AdjustZAxist;
            Quaternion Zdirection = Quaternion.Euler(0,90,AdjustZAxist);
            this.gameObject.transform.rotation = Zdirection;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null && other.gameObject == CollideEnemy)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
