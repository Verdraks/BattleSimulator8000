﻿using UnityEngine;
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

    public Transform attackAnchor;
}

public class LifeData : IQueryData
{
    public LifeData() => CurrentLife = MaxLife;

    public short MaxLife;
    public short CurrentLife;
}