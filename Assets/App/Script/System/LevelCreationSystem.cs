using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelCreationSystem", menuName = "SO/Systems/LevelCreationSystem")]
public class LevelCreationSystem : ScriptableObjectSystem
{
    [Header("References")]
    [SerializeField] private GameObject environmentLevelPrefab;
    [SerializeField] private GameObject levelContentPrefab;

    private readonly List<GameObject> _instancesLevel = new();
    
    public override void Enable()
    {
        InstantiateContent(environmentLevelPrefab);
        InstantiateContent(levelContentPrefab);
        
        base.Enable();
    }

    private void InstantiateContent(GameObject contentPrefab)
    {
        if (contentPrefab)
        {
            var asyncOperationLevel = InstantiateAsync(contentPrefab);
            _instancesLevel.AddRange(asyncOperationLevel.Result);
        }
    }

    public override void Disable()
    {
        foreach (var instance in _instancesLevel)
        {
            Destroy(instance);
        }
        _instancesLevel.Clear();
        
        base.Disable();
    }
}