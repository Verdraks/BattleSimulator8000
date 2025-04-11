using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackSystem", menuName = "SO/Systems/AttackSystem")]
public class AttackSystem : ScriptableObjectSystem
{
    [Title("References")]
    [SerializeField] private QuerySystem querySystem;
    
    public override void Execute()
    {
        BatchMeleeUnits();
        BatchDistanceUnits();
    }

    private void BatchMeleeUnits()
    {
        if (querySystem.GetData<MovementData, AttackData, UnitMeleeTag>(out var unitsMeleeData))
        {
            foreach (var unit in unitsMeleeData)
            {
                if (!querySystem.EntityIsValid(unit.Item1.EntityTarget)) continue;
                
                querySystem.GetData<TransformData>(unit.Item1.EntityTarget, out var targetEntityTransform ); 
                
                float dist = Vector3.Distance(unit.Item1.NavMeshAgent.transform.position, targetEntityTransform.Transform.position);
                if (dist > unit.Item1.NavMeshAgent.stoppingDistance) continue;
                
                if (Time.time - unit.Item2.LastAttackTime < unit.Item2.Cooldown) continue;
                
                if (querySystem.GetEntity(unit.Item1, out var entity))
                {
                    querySystem.AddData<UnitDamageTag>(entity, new UnitDamageTag()
                    {
                        DamageReceive = 1
                    });
                }
                
            }
        }
    }

    private void BatchDistanceUnits()
    {
        if (querySystem.GetData<MovementData, AttackData, AttackDistanceData>(out var unitsRangedData))
        {
            foreach (var unit in unitsRangedData)
            {
                if (!querySystem.EntityIsValid(unit.Item1.EntityTarget)) continue;
                
                querySystem.GetData<TransformData>(unit.Item1.EntityTarget, out var targetEntityTransform ); 
                
                float dist = Vector3.Distance(unit.Item1.NavMeshAgent.transform.position, targetEntityTransform.Transform.position);
                if (dist > unit.Item1.NavMeshAgent.stoppingDistance) continue;
                
                if (Time.time - unit.Item2.LastAttackTime < unit.Item2.Cooldown) continue;

                if (unit.Item3.PrefabProjectile)
                    InstantiateAsync(unit.Item3.PrefabProjectile, unit.Item3.attackAnchor.position, unit.Item3.attackAnchor.rotation);

                unit.Item2.LastAttackTime = Time.time;
            }
        }
    }
}