using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Dialogue
{
    public string npcName;
    public string[] sentence;
    public Dialogue(){

    }
    public Dialogue(string[] sentence){
        this.sentence = sentence;
    }
}
