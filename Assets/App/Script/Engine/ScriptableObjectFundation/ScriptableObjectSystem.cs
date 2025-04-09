using Sirenix.OdinInspector;
using UnityEngine;

public abstract class ScriptableObjectSystem : ScriptableObjectDynamic
{
    [Title("Settings")] 
    [SerializeField] private UpdateType updateType;
    
    public UpdateType getUpdateType => updateType;
    
    public virtual void Execute(){}

    public enum UpdateType
    {
        Update,
        LateUpdate,
        FixedUpdate,
        DynamicUpdate
    }
}
