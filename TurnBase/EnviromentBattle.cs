using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentBattle : MonoBehaviour
{
    [SerializeField]List<GameObject> enviroments;
    void Start()
    {
        enviroments[Sender.enviromentID].gameObject.SetActive(true);
    }
}
