
using Sirenix.OdinInspector;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthSystem", menuName = "SO/Systems/HealthSystem")]
public class HealthSystem : ScriptableObjectSystem
{
    [Title("Settings")]
    [SerializeField] private AnimationCurve animationCurveDamage;
    
    [Title("References")]
    [SerializeField] private QuerySystem querySystem;

    
    
    public override void Execute()
    {
        BatchDamageEntity();
    }

    private void BatchDamageEntity()
    {
        if (querySystem.GetData<TransformData, UnitDamageTag>(out var unitsDamageTag))
        {
            float valueLerp;
            
            foreach (var unit in unitsDamageTag)
            {
                valueLerp = animationCurveDamage.Evaluate( Mathf.Lerp(0, 1, unit.Item2.LerpInternal));
                unit.Item2.LerpInternal += Time.deltaTime;
                unit.Item1.Transform.localScale = new Vector3(valueLerp,valueLerp,valueLerp);

                if (unit.Item2.LerpInternal > 1f)
                {
                    querySystem.GetEntity(unit.Item2, out var entity);
                    querySystem.RemoveData<UnitDamageTag>(entity, unit.Item2);
                }
            }
        }
    }
}