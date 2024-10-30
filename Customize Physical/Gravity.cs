using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
     public float gravity = 9.8f;

    public float groundDistance = 0.1f;
    public float AdditionalDistance;
    public LayerMask groundMask;

    private Collider col;
    private float fallSpeed = 0f;
    private bool isground;
    private bool isOverLapse;
    void Start()
    {
        // Get the collider attached to the GameObject
        col = gameObject.GetComponent<Collider>();
    }

    void Update()
    {
        // Calculate the center of the collider
        Vector3 colliderCenter = col.bounds.center;

        // Cast a ray from the center of the collider towards the ground
         isground = Physics.CheckSphere(colliderCenter + Vector3.down * (groundDistance - AdditionalDistance), AdditionalDistance, groundMask);
            

        if (!isground)
        {
            // Apply gravity
            fallSpeed -= gravity * Time.deltaTime;
        }
        else
        {
            fallSpeed = 0f;
        }

        // Move the object based on gravity
        transform.position += new Vector3(0f, fallSpeed * Time.deltaTime, 0f);
    }
     private void OnDrawGizmos()
    {
        if(col != null){
        // Visualize the ray
        Gizmos.color = Color.red;
        Vector3 colliderCenter = col.bounds.center;
        Gizmos.DrawWireSphere(colliderCenter + Vector3.down * (groundDistance - AdditionalDistance), AdditionalDistance);

        }
    }
    public bool Checkground(){
        return isground;
    }
    
}
