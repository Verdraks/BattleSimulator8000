using UnityEngine;

/// <summary>
/// Define data implementation that can be added into the Query
/// </summary>
public interface IQueryData
{ 
    void Initialize(ref QuerySystem querySystem, int idEntity);
    void Deinitialize(ref QuerySystem querySystem, int idEntity);
}