using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Create Instance of Scriptable Object Data and manage Initialization/Deinitialization
/// </summary>
/// <typeparam name="T">Type of Data Instantiated</typeparam>
public abstract class HookerData<T> : MonoBehaviour where T : IQueryData, new()
{
    [Title("Reference")]
    [SerializeField] protected QuerySystem querySystem;
    
    protected T Data;
    
    protected virtual void Awake()
    {
        Data = new T();
        Data.Initialize(ref querySystem, gameObject.GetInstanceID());
    }

    protected virtual void OnDestroy()
    {
        Data.Deinitialize(ref querySystem, gameObject.GetInstanceID());
    }
}
