using System;
using UnityEngine;
using UnityEngine.AI;

public class MeleeUnitBaker : BakerData
{
    [Header("Settings")] 
    [SerializeField] private short life = 5;
    [SerializeField] private byte layerTeam;
    [SerializeField] private float detectionRange = 1000;
    [SerializeField] private float cooldown = 1.5f;
    
    [Header("References")]
    [SerializeField] private NavMeshAgent navMeshAgent;

    protected override void Bake()
    {
        var movementData = new MovementData
        {
            NavMeshAgent = navMeshAgent,
            DetectionRadius = detectionRange,
        };

        var transformData = new TransformData()
        {
            Transform = transform
        };

        var teamData = new UnitTeam()
        {
            Team = layerTeam
        };

        var attackData = new AttackData()
        {
            Cooldown = cooldown
        };

        var lifeData = new LifeData()
        {
            MaxLife = life,
            CurrentLife = life
        };
        
        querySystem.AddData<MovementData>(ID,movementData);
        querySystem.AddData<TransformData>(ID,transformData);
        querySystem.AddData<UnitTeam>(ID,teamData);
        querySystem.AddData<AttackData>(ID, attackData);
        querySystem.AddData<UnitMeleeTag>(ID, new UnitMeleeTag());
        querySystem.AddData<LifeData>(ID,lifeData);
    }
}