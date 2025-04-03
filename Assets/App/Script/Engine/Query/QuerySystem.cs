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
        Debug.Log("pass");
    }

    public override void Disable()
    {
        Debug.Log("unpass");
        queryData.Disable();
        base.Disable();
    }


    public bool GetData<T>(out LinkedList<T> data) where T: IQueryData , new()
    {
        data = new LinkedList<T>();
        
        foreach (var datasEntity in queryData.Data.Values)
        {
            foreach (var dataEntity in datasEntity)
            {
                if (dataEntity.Key == typeof(T))
                {
                    data.AddLast((T)dataEntity.Value);
                }
            }
        }
        
        return data.Count > 0;
    }

    public bool GetUniqueData<T>(out T data) where T: IQueryData , new()
    {
        data = default(T);
        
        if (queryData.Data.Values.Count(o => o.GetType() == typeof(T)) == 1)
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


    public bool AddData<T>(int idEntity, object dataEntity) where T: IQueryData , new()
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