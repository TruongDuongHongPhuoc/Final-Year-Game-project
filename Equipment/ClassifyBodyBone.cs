using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum bodyClassify{
    head, 
LeftShoulder,leftUpperArm, LeftLowerArm,LeftHand,LeftFinger,
 RightShoulder,RightUpperArm,RightLowerArm,RightHand,RightFinger,
 lowerSpine,UpperSprine,
 UnderSprine,
 LeftUpperLeg, LeftLeg,LeftFoot,LeftTose,
 RIghtUpperLeg, RightLeg,RightFoot ,RightTose,
 other
 }
public class ClassifyBodyBone : MonoBehaviour
{
    public bodyClassify bodyClassify;
}
