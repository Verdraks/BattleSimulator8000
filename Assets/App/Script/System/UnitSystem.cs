using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitSystem", menuName = "SO/Systems/UnitSystem")]
public class UnitSystem : ScriptableObjectSystem
{
    [Title("Settings")]
    [SerializeField] private LayerMask layerMaskUnits;
    
    [Title("References")]
    [SerializeField] private QuerySystem querySystem;
    
    
    public override void Execute()
    {
        if (querySystem.GetData<TransformData, MovementData, UnitTeam>(out var unitsData))
        {
            DetectionSystem(ref unitsData);
            MovementSystem(ref unitsData);
        }
    }

    private void DetectionSystem(ref List<Tuple<TransformData,MovementData, UnitTeam>> unitsData)
    {
        foreach (var unit in unitsData)
        {
            bool hasEntityTarget = querySystem.EntityIsValid(unit.Item2.EntityTarget);
            
            if (hasEntityTarget)
            {
                querySystem.GetData<TransformData>(unit.Item2.EntityTarget, out var entityTransformData);
                if (Vector3.Distance(unit.Item1.Transform.position, entityTransformData.Transform.position) < unit.Item2.DetectionRadius) continue;
            }
            
            float bestDistSqr = Mathf.Infinity;
            TransformData closest = null;

            Vector3 selfPos = unit.Item1.Transform.position;
            float detectionRadiusSqr = unit.Item2.DetectionRadius * unit.Item2.DetectionRadius;

            foreach (var target in unitsData)
            {
                if (Equals(target, unit) || target.Item3.Team == unit.Item3.Team) continue;

                float distSqr = (target.Item1.Transform.position - selfPos).sqrMagnitude;
                if (distSqr < detectionRadiusSqr && distSqr < bestDistSqr)
                {
                    bestDistSqr = distSqr;
                    closest = target.Item1;
                }
            }
            if (closest == null) continue;
            querySystem.GetEntity(closest, out var entityId);
            unit.Item2.EntityTarget = entityId;
        }
        
    }

    private void MovementSystem(ref List<Tuple<TransformData,MovementData, UnitTeam>> unitsData)
    {
        foreach (var unit in unitsData)
        {
            if (!querySystem.EntityIsValid(unit.Item2.EntityTarget)) continue;
            
            querySystem.GetData<TransformData>(unit.Item2.EntityTarget, out var entityTransformData);
            
            var dist = Vector3.Distance(unit.Item1.Transform.position, entityTransformData.Transform.position);
            if (dist > unit.Item2.NavMeshAgent.stoppingDistance)
            {
                unit.Item2.NavMeshAgent.SetDestination(entityTransformData.Transform.position);
            }
            else
            {
                unit.Item2.NavMeshAgent.ResetPath();
            }
        }
    }
}