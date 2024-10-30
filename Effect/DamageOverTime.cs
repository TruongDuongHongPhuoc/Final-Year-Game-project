using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Damage Over Turn", menuName = "Bad Status System/ New Effect")]
public class DamageOverTime : EffectBase
{
    [SerializeField] GameObject effect;
    public override void EffectDeal(UnitBase unit)
    {
        unit.CurrentHeal -= 3;
        Debug.Log("HP minus due to Brush");
        Instantiate(effect, unit.transform.position, Quaternion.identity);
    }
}
