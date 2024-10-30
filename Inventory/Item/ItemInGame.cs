using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInGame : MonoBehaviour
{
    [SerializeField] private ItemBase itemBase;
    public float rotationSpeed = 30f;
    private void Update() {
         transform.Rotate(new Vector3(0f,0f,rotationSpeed*Time.deltaTime),Space.Self);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<PlayerController>()!=null){
            other.gameObject.GetComponent<PlayerController>().collectItem(itemBase);
            Destroy(gameObject);
        }
    }
}
