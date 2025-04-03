using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "QueryData", menuName = "SO/Engine/QueryData")]
public class QueryData : ScriptableObjectDynamic
{
    [ShowInInspector, ReadOnly] public Dictionary<int, Dictionary<Type, object>> Data;

    public override void Enable()
    {
        base.Enable();
        Data = new Dictionary<int, Dictionary<Type, object>>();
        Debug.Log("pass");
    }

    public override void Disable()
    {
        Data = null;
        base.Disable();
    }
}