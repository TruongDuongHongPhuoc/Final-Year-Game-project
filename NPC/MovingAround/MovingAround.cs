using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAround : MonoBehaviour
{
   [SerializeField]public BoxCollider MovingArea;
   public Vector3 pickLocation;
   public float WaitBeforeMoving;
   public float speed;
    public bool isMoving;
    public bool isWaitForNewPosition;
    private Animator animator;
    public bool isHavingAnimation;
    private void Start() {
         animator = GetComponentInChildren<Animator>(); 
          CheckArea();
        pickLocation = PickRamDomLocation();
    }
   void Walking()
    {
        RotateToPickPosition();
        isMoving = true;
       Vector3 direction = (pickLocation - transform.position).normalized;
       direction.y =0;
       if(Vector3.Distance(transform.position, pickLocation) > 0.2f){
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        if(isHavingAnimation)
        animator.SetBool("isMoving",true);
       }
        // Check if the NPC is close enough to the target position
        if (Vector3.Distance(transform.position, pickLocation) <= 0.2f)
        {
            isMoving = false;
            if(isWaitForNewPosition == false){
             RanDomPosition();
            }
            // Stop animation if needed
            if (animator != null)
            {
                animator.SetBool("isMoving", false);
                
            }
            // Start the coroutine to pick a new random position
           
        }
    }
    private void Update() {
       CheckArea();
        Walking();
    }

   public void RanDomPosition(){
    StartCoroutine(startRamdomize());
   }
   IEnumerator startRamdomize(){
        isWaitForNewPosition = true;
        yield return new WaitForSeconds(WaitBeforeMoving);
        isWaitForNewPosition = false;
    pickLocation = PickRamDomLocation();
   }

   public Vector3 PickRamDomLocation(){
    Vector3 ReturnVector = new Vector3(1,1,1);
     if(MovingArea !=null){
        Bounds bounds = MovingArea.bounds;
        ReturnVector.x = Random.Range(bounds.min.x, bounds.max.x);
        // ReturnVector.y = Random.Range(bounds.min.y, bounds.max.y);
        //if y != 0 it will fly away :)))
        ReturnVector.y = transform.position.y;
        ReturnVector.z = Random.Range(bounds.min.z, bounds.max.z);

     }
     return ReturnVector;
   }
   public void RotateToPickPosition(){
    Vector3 direction = pickLocation - transform.position;
    direction.y = 0;
    this.transform.rotation = Quaternion.LookRotation(direction);
   }
   private void OnTriggerEnter(Collider other) {
        if(other !=null){
            pickLocation = transform.position;
        }
   }
   public void CheckArea(){
   if (MovingArea == null)
        {
            
            Vector3 rayOrigin = transform.position;
            Vector3 rayDirection = Vector3.down;
            RaycastHit[] hits = Physics.RaycastAll(rayOrigin,rayDirection,5f);
            foreach(RaycastHit hit in hits){
                if(hit.collider.gameObject.GetComponent<BoxCollider>()!= null){
                    MovingArea = hit.collider.gameObject.GetComponent<BoxCollider>();
                }
            }
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position,transform.position + Vector3.down * 5f);
    }
}
