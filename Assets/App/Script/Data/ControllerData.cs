using UnityEngine;

public class InputData : IQueryData
{
    public Vector2 DirectionMovement;
    public Vector2 DirectionRotation;
    public bool Sprint;
    
    public int IndexPrefabs;

    public bool InstantiateUnit;
    public bool DestroyUnit;

    public bool Validate;
}

public class CameraData : IQueryData
{
    public Camera Camera;

    public float Pitch;
    public float Yaw;
}