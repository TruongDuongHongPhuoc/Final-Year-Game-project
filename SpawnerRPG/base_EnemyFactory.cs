using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  abstract class base_EnemyFactory : ScriptableObject
{
    [SerializeField] public List<ItemBase> PossibleReward;
    [SerializeField] public List<UnitBase> EnemyPossible;
    [SerializeField] public int AdditionEXP;
    [SerializeField] public int AdditionMoney;
   public abstract List<UnitBase> CreateUnit();
   public abstract List<ItemBase> CreateItem();
   public abstract int RamdomInt(int min, int max);
   public abstract Enemy GenerateEnemy(Enemy enemy);
}
