using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Paralyse", menuName = "Bad Status System/ New Effect/ Paralyze")]
public class Paralyse : EffectBase
{
    public float Percent;
    [SerializeField] GameObject effect;
    public override void EffectDeal(UnitBase unit)
    {
        Vector3 position = unit.transform.position + Vector3.up;
        Instantiate(effect, position, Quaternion.identity);
        if(isParalyseThisTurn()){
        unit.isAttackble = false;
        }
    }
    public override void TurnRemainDecrease(UnitBase unit)
    {
        base.TurnRemainDecrease(unit);
        unit.isAttackble = true;
    }
    public bool isParalyseThisTurn(){
        Percent = Mathf.Clamp(Percent, 0, 100);
        float value = Random.Range(0, 100);
        return Percent < value;
    }
}
