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
        if (!query.GetData<RotationData>(out var rotationData)) return;
        foreach (var data in rotationData)
        {
            data.Transform.Rotate(Vector3.up,rotationSpeed*Time.deltaTime);
        }
    }
}