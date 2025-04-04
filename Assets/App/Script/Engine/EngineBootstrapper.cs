using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Load requirement Resources during the initialization 
/// </summary>
public class EngineBootstrapper : ScriptableObject
{
    private static string _pathDependenciesPrefab = "Prefabs/Dependencies";
    private static string _pathBootstrapperPrefab = "Prefabs/Bootstrapper";
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialization()
    {
        LoadObject(_pathDependenciesPrefab);
        LoadObject(_pathBootstrapperPrefab);
    }

    private static void LoadObject(string path)
    {
        if (path == null)
        {
            Debug.LogError($"Path Load Object {(string)null} is null");
            return;
        }
        var obj = Resources.Load(path);
        if (obj) Instantiate(obj);
        else Debug.LogError($"Try Load Object: {path}, but not exist");
    }
}
