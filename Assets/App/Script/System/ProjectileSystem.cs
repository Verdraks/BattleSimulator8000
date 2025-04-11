using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "ProjectileSystem", menuName = "SO/Systems/ProjectileSystem")]
public class ProjectileSystem : ScriptableObjectSystem
{
    [Title("References")]
    [SerializeField] private QuerySystem querySystem;

    private const float SizeBoxDetection = 2.5f;
    
    public override void Execute()
    {
        if (querySystem.GetData<TransformData,ProjectileTag>(out var projectilesData))
        {
            foreach (var projectile in projectilesData)
            {
                projectile.Item1.Transform.position += projectile.Item1.Transform.forward * (projectile.Item2.Speed * Time.deltaTime);
                
            }

            if (querySystem.GetData<TransformData,MovementData>(out var transformsData))
            {
                foreach (var projectile in projectilesData)
                {
                    foreach (var transformData in transformsData)
                    {
                        if (projectile.Item1.Transform == transformData.Item1.Transform) continue;

                        if (Vector3.Distance(projectile.Item1.Transform.position, transformData.Item1.Transform.position) <=
                            SizeBoxDetection)
                        {
                            if (querySystem.GetEntity(transformData.Item1.Transform, out var entity))
                            {
                                querySystem.AddData<UnitDamageTag>(entity, new UnitDamageTag()
                                {
                                    DamageReceive = 1
                                });
                                
                                Destroy(projectile.Item1.Transform.gameObject);
                            }
                        }
                    }
                }
            }
            
        }
    }
}