using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class BakerData: MonoBehaviour
{
    [Title("References")]
    [SerializeField] protected QuerySystem querySystem;

    protected int ID;

    protected void Awake()
    {
        ID = gameObject.GetInstanceID();
        Bake();
    }

    protected abstract void Bake();

    protected virtual void OnDestroy() => querySystem.RemoveEntity(ID);
}