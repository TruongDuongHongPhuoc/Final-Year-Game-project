using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempTestScript : MonoBehaviour
{
 public GameObject PlayerChest;
public GameObject PlayerStomach;
public GameObject PlayerLeftShoulder;
public GameObject PlayerLeftUpperarm;
public GameObject PlayerLeftArm;
public GameObject PlayerLeftHand;
public GameObject PlayerLeftFing;
public GameObject PlayerRightShoulder;
public GameObject PlayerRightUpperarm;
public GameObject PlayerRightArm;
public GameObject PlayerRightHand;
public GameObject PlayerRightFing;
[TextArea] public string somehtin;
public GameObject ArmorChest;
public GameObject ArmorStomach;
public GameObject ArmorLeftShoulder;
public GameObject ArmorLeftUpperarm;
public GameObject ArmorLeftArm;
public GameObject ArmorLeftHand;
public GameObject ArmorLeftFing;
public GameObject ArmorRightShoulder;
public GameObject ArmorRightUpperarm;
public GameObject ArmorRightArm;
public GameObject ArmorRightHand;
public GameObject ArmorRightFing;
public Animator animator;
private void Start() {
    animator = GetComponent<Animator>();
    animator.SetBool("isMoving",true);
}}