using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class HookerSystem : MonoBehaviour
{
    [Title("References")]
    [SerializeField] private ScriptableObjectSystem[] scriptableObjectSystems;

    private Dictionary<ScriptableObjectSystem.UpdateType, ScriptableObjectSystem[]> _scriptableObjectSystemsCache;
    
    private void Awake()
    {
        _scriptableObjectSystemsCache = new Dictionary<ScriptableObjectSystem.UpdateType, ScriptableObjectSystem[]>();
        
        foreach (ScriptableObjectSystem.UpdateType updateType in Enum.GetValues(
                     typeof(ScriptableObjectSystem.UpdateType)))
        {
            _scriptableObjectSystemsCache.Add(updateType, 
                scriptableObjectSystems.Where(o=> o.getUpdateType == updateType)
                    .ToArray());
        }
    }

    private void OnEnable()
    {
        foreach (var system in scriptableObjectSystems) system.Enable();
    }

    private void OnDisable()
    {
        foreach (var system in scriptableObjectSystems) system.Disable();
    }

    private void Update() => IterateSystems(ScriptableObjectSystem.UpdateType.Update);

    private void FixedUpdate() => IterateSystems(ScriptableObjectSystem.UpdateType.FixedUpdate);

    private void LateUpdate() => IterateSystems(ScriptableObjectSystem.UpdateType.LateUpdate);

    private void IterateSystems(ScriptableObjectSystem.UpdateType updateType)
    {
        foreach (var system in _scriptableObjectSystemsCache[updateType]) system.Update();
    }
}
