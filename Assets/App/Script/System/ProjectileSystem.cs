using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "ProjectileSystem", menuName = "SO/Systems/ProjectileSystem")]
public class ProjectileSystem : ScriptableObjectSystem
{
    [Title("References")]
    [SerializeField] private QuerySystem querySystem;

    private const float SizeBoxDetection = 1f;
    private const short DamageProjectile = 3;

    public override void Execute()
    {
        if (querySystem.GetData<TransformData,ProjectileTag>(out var projectilesData))
        {
            foreach (var projectile in projectilesData)
            {
                projectile.Item1.Transform.position += projectile.Item1.Transform.forward * (projectile.Item2.Speed * Time.deltaTime);
                
                projectile.Item2.LifeSpan += Time.deltaTime;
                
                if (projectile.Item2.LifeSpan > ProjectileTag.LifeTime) Destroy(projectile.Item1.Transform.gameObject);
                
            }

            if (querySystem.GetData<TransformData,MovementData>(out var transformsData))
            {
                foreach (var projectile in projectilesData)
                {
                    Vector2 projectilePosition = new Vector2(projectile.Item1.Transform.position.x, projectile.Item1.Transform.position.z);
                    
                    foreach (var transformData in transformsData)
                    {
                        
                        Vector2 transformPosition = new Vector2(transformData.Item1.Transform.position.x, transformData.Item1.Transform.position.z);
                        
                        if (Vector2.Distance(projectilePosition, transformPosition) <=
                            SizeBoxDetection)
                        {
                            if (querySystem.GetEntity(transformData.Item1, out var entity))
                            {
                                querySystem.AddData<UnitDamageTag>(entity, new UnitDamageTag()
                                {
                                    DamageReceive = DamageProjectile
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