using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "QuerySystem", menuName = "SO/Engine/QuerySystem")]
public sealed class QuerySystem : ScriptableObjectSystem
{
    [Title("Reference")]
    [SerializeField] private QueryData queryData;

    public override void Enable()
    {
        base.Enable();
        queryData.Enable();
    }

    public override void Disable()
    {
        queryData.Disable();
        base.Disable();
    }


    public bool GetData<T>(out List<T> data) where T: IQueryData
    {
        data = new List<T>();
        
        foreach (var datasEntity in queryData.Data.Values)
        {
            if (datasEntity.TryGetValue(typeof(T), out var obj))
            {
                data.Add((T)obj);
            }
        }
        
        return data.Count > 0;
    }

    public bool GetData<T, T1>(out List<Tuple<T,T1>> data) where T : IQueryData where T1 : IQueryData
    {
        data = new List<Tuple<T,T1>>();
        
        foreach (var datasEntity in queryData.Data.Values)
        {
            if (datasEntity.TryGetValue(typeof(T), out var objT) && datasEntity.TryGetValue(typeof(T1), out var objT1))
            {
                data.Add(new Tuple<T, T1>((T)objT, (T1) objT1));
            }
        }
        return data.Count > 0;
    }
    
    public bool GetData<T, T1,T2>(out List<Tuple<T,T1,T2>> data) where T : IQueryData where T1 : IQueryData where T2 : IQueryData
    {
        data = new List<Tuple<T,T1,T2>>();
        
        foreach (var datasEntity in queryData.Data.Values)
        {
            if (datasEntity.TryGetValue(typeof(T), out var objT) 
                && datasEntity.TryGetValue(typeof(T1), out var objT1) 
                && datasEntity.TryGetValue(typeof(T2), out var objT2))
            {
                data.Add(new Tuple<T, T1,T2>((T)objT, (T1) objT1, (T2) objT2));
            }
        }
        return data.Count > 0;
    }

    public bool GetUniqueData<T>(out T data) where T: IQueryData
    {
        data = default(T);
        
        if (queryData.Data.Values.Count(o => o.Keys.Contains(typeof(T))) == 1)
        {
            foreach (var datasEntity in queryData.Data.Values)
            {
                foreach (var dataEntity in datasEntity)
                {
                    if (dataEntity.Key == typeof(T))
                    {
                        data = (T)dataEntity.Value;
                        return true;
                    }
                }
            }  
        }
        
        return false;
    }


    public bool AddData<T>(int idEntity, object dataEntity) where T: IQueryData
    {
        if (queryData.Data == null) return false;
        if (!queryData.Data.Keys.Contains(idEntity))
        {
            queryData.Data.Add(idEntity, new Dictionary<Type, object>());
        }
        
        else if (queryData.Data[idEntity].ContainsKey(dataEntity.GetType())) return false;
        
        queryData.Data[idEntity].Add(typeof(T), dataEntity);
        return true;
        
    }

    public bool RemoveData<T>(int idEntity, object dataEntity)
    {
        if (queryData.Data == null) return false;
        if (!queryData.Data.Keys.Contains(idEntity)) return false;
        if (!queryData.Data[idEntity].ContainsKey(dataEntity.GetType())) return false;
        
        queryData.Data[idEntity].Remove(typeof(T));
        if (queryData.Data[idEntity].Count == 0) queryData.Data.Remove(idEntity);
        
        return true;
    }
}