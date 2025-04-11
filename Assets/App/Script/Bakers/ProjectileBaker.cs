
using Sirenix.OdinInspector;
using UnityEngine;

public class ProjectileBaker : BakerData
{
    [Title("Settings")]
    [SerializeField] private float speed;
    
    protected override void Bake()
    {

        var projectileData = new ProjectileTag()
        {
            Speed = speed,
        };
        
        querySystem.AddData<ProjectileTag>(ID, projectileData);
    }
}