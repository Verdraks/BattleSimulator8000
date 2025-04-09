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
}

public class AttackDistanceData : IQueryData
{
    public GameObject PrefabProjectile;
}

public class LifeData : IQueryData
{
    public short MaxLife;
    public short CurrentLife;
}