using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDonnotDestroyOnLoad : MonoBehaviour
{
    private void Start() {
        DontDestroyOnLoad(this.gameObject);
    }
}
