using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RotationSystem", menuName = "SO/System/Rotation System")]
public class RotationSystem : ScriptableObjectSystem
{
    [Title("Settings")]
    [SerializeField] private float rotationSpeed;
    
    [Title("Reference")]
    [SerializeField] private QuerySystem query;
    
    public override void Update()
    {
        if (!query.GetData<RotationTag, TransformData>(out var rotationData)) return;
        foreach (var data in rotationData)
        {
            data.Item2.Transform.Rotate(Vector3.up,rotationSpeed*Time.deltaTime);
        }
    }
}