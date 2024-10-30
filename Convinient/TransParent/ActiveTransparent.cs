using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTransparent : MonoBehaviour
{
    private void OnTriggerStay(Collider other) {
        Debug.Log("camera triggger stay work ");
        if(other.gameObject.CompareTag("transparentable")){
            Debug.Log("Working");
            other.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }else{

        }
    }
    private void OnTriggerExit(Collider other) {
        Debug.Log("camera triggger eixt work ");
        if(other.gameObject.tag == "transparentable"){
            Debug.Log("Working");
            other.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
    }
    
    }
