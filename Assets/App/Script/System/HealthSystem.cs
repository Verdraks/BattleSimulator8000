
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthSystem", menuName = "SO/Systems/HealthSystem")]
public class HealthSystem : ScriptableObjectSystem
{
    [Title("Settings")]
    [SerializeField] private AnimationCurve animationCurveDamage;
    
    [Title("References")]
    [SerializeField] private QuerySystem querySystem;

    
    
    public override void Execute()
    {
        if (querySystem.GetData<TransformData, UnitDamageTag, LifeData>(out var unitsDamageTag))
        {
            BatchDamageReceive(unitsDamageTag);
            BatchDamageFeedback(unitsDamageTag);
        }
    }

    private void BatchDamageReceive(List<Tuple<TransformData, UnitDamageTag, LifeData>> unitsDamageTag)
    {
        foreach (var unit in unitsDamageTag)
        {
            if (unit.Item2.LerpInternal <= 0)
            {
                unit.Item3.CurrentLife -= unit.Item2.DamageReceive;
                
                if (unit.Item3.CurrentLife <= 0)
                {
                    Destroy(unit.Item1.Transform.gameObject);
                }
                
            }
            else if (unit.Item2.LerpInternal >= 1)
            {
                querySystem.GetEntity(unit.Item2, out var entity);
                querySystem.RemoveData<UnitDamageTag>(entity, unit.Item2);
            }
            
        }
    }
    
    private void BatchDamageFeedback(List<Tuple<TransformData, UnitDamageTag, LifeData>> unitsDamageTag)
    {
        float valueLerp;

        foreach (var unit in unitsDamageTag)
        {
            valueLerp = animationCurveDamage.Evaluate( Mathf.Lerp(0, 1, unit.Item2.LerpInternal));
            unit.Item2.LerpInternal += Time.deltaTime;
            unit.Item1.Transform.localScale = new Vector3(valueLerp,valueLerp,valueLerp);

        }
    }
}