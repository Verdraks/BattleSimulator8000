using UnityEngine;

public class RotationData : IQueryData
{
    public Transform Transform;
    
    public void Initialize(ref QuerySystem querySystem, int idEntity)
    {
        Debug.Log(querySystem.AddData<RotationData>(idEntity,this));
    }

    public void Deinitialize(ref QuerySystem querySystem, int idEntity)
    {
        Debug.Log(querySystem.RemoveData<RotationData>(idEntity, this));
    }
}