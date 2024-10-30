using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sender
{   
    public static List<UnitBase> enemy;
    public static UnitBase playerUnit;
    public static bool isWin;
    public static Enemy enemyBattling;
    public static int enviromentID;
    public static void OnReact(){
        if(isWin){
            enemyBattling.Defeated();
        }else{
            Debug.Log("Player Lost");
        }
    }
}
