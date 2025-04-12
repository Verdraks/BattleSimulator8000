using UnityEngine;
using UnityEngine.AI;

public class MovementData: IQueryData
{
    public NavMeshAgent NavMeshAgent;
    public int EntityTarget;
    
    public float DetectionRadius;
}

public class AttackData : IQueryData
{
    public float Cooldown;
    public float LastAttackTime;
    public short AttackDamage;
}

public class AttackDistanceData : IQueryData
{
    public GameObject PrefabProjectile;

    public Transform AttackAnchor;
}

public class LifeData : IQueryData
{
    public LifeData() => CurrentLife = MaxLife;

    public short MaxLife;
    public short CurrentLife;
}