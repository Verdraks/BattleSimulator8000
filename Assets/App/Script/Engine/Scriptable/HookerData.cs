using System;
using UnityEngine;

public abstract class HookerData<T> : MonoBehaviour where T : DataScriptableObject<T>
{
    protected T Data;

    protected virtual void Awake() => Data = ScriptableObject.CreateInstance<T>();

    protected virtual void OnDestroy() => Destroy(Data);
}
