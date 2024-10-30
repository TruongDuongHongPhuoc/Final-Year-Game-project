using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Factory", menuName = "Spawner/EnemyFactory", order = 1)]
public class EnemyFactory : base_EnemyFactory
{
    public int minLevel = 3;
    public int maxLevel = 6;
    public int minAdditionalEXP = 40;
    public int maxLevelAdditionalEXP = 100;
    public int minAdditionMoney = 20;
    public int maxAdditionMoney = 80;
    public override List<ItemBase> CreateItem()
    {
        List<ItemBase> listReturn = new List<ItemBase>();
        listReturn.Add(PossibleReward[UnityEngine.Random.Range(0, PossibleReward.Count)]);
        return listReturn;
    }

    public override List<UnitBase> CreateUnit()
    {
       UnitBase Spawned = Instantiate(EnemyPossible[UnityEngine.Random.Range(0, EnemyPossible.Count)]);
       Spawned.PowerUpTolevel(UnityEngine.Random.Range(minLevel, maxLevel));
       List<UnitBase> unitBases = new List<UnitBase>();
       unitBases.Add(Spawned);
       return unitBases;
    }
    public override int RamdomInt( int min, int max)
    {
        int amount = UnityEngine.Random.Range(min,max);
        return amount;
    } 

    public override Enemy GenerateEnemy(Enemy enemy)
    {
        enemy.Enemies = CreateUnit();
        enemy.ItemReward = CreateItem();
        enemy.additionExp = RamdomInt(minAdditionalEXP, maxAdditionMoney);
        enemy.AdditionMoney = RamdomInt(minAdditionMoney, maxAdditionMoney);
        return enemy;
    }

    public List<Type> getEnemyType()
    {
        List<Type> enemyTypes = new List<Type>();
        foreach (UnitBase enemy in EnemyPossible)
        {
            enemyTypes.Add(enemy.GetType());
        }
        return enemyTypes;
    }
}
