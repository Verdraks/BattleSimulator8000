
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class RangeUnitBaker : BakerData
{
    [Header("Settings")] 
    [SerializeField] private short life = 5;
    [SerializeField] private byte layerTeam;
    [SerializeField] private float detectionRange = 1000;
    [SerializeField] private float cooldown = 1.5f;
    
    [Header("References")]
    [SerializeField] private NavMeshAgent navMeshAgent;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileAnchor;
    
    
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

        var attackDistanceData = new AttackDistanceData()
        {
            attackAnchor = projectileAnchor,
            PrefabProjectile = projectilePrefab
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
        querySystem.AddData<AttackDistanceData>(ID, attackDistanceData);
        querySystem.AddData<UnitRangedTag>(ID, new UnitRangedTag());
        querySystem.AddData<LifeData>(ID,lifeData);
    }
}