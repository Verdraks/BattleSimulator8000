using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelCreationSystem", menuName = "SO/Systems/LevelCreationSystem")]
public class LevelCreationSystem : ScriptableObjectSystem
{
    [Header("References")]
    [SerializeField] private GameObject levelContentPrefab;

    private readonly List<GameObject> _instancesLevel = new();
    
    public override void Enable()
    {
        base.Enable();
        InstantiateContent(levelContentPrefab);
    }

    private void InstantiateContent(GameObject contentPrefab)
    {
        if (!contentPrefab) return;
        var asyncOperationLevel = InstantiateAsync(contentPrefab);
        asyncOperationLevel.completed += _ => _instancesLevel.AddRange(asyncOperationLevel.Result);
    }

    public override void Disable()
    {
        base.Disable();
        foreach (var instance in _instancesLevel)
        {
            Destroy(instance);
        }
        _instancesLevel.Clear();
        
    }
}