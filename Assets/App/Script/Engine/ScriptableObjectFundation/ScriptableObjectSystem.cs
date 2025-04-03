using Sirenix.OdinInspector;
using UnityEngine;

public abstract class ScriptableObjectSystem : ScriptableObjectDynamic
{
    [Title("Settings")] 
    [SerializeField] private UpdateType updateType;
    
    public UpdateType getUpdateType => updateType;
    
    public virtual void Update(){}

    public enum UpdateType
    {
        Update,
        LateUpdate,
        FixedUpdate,
        DynamicUpdate
    }
}
