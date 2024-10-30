using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
[Serializable]
public class PlayerData
{
    public string sceneName;
    public Vector3 position;
    public int Money;
    public int skillPoint;
    public string RPGCharacterBaseID; 

    public PlayerData(string sceneName, Vector3 position, int money, int skillPoint, string RPGCharacterBaseID)
    {
        this.sceneName = sceneName;
        this.position = position;
        this.Money = money;
        this.skillPoint = skillPoint;
        this.RPGCharacterBaseID = RPGCharacterBaseID;
    }
}
