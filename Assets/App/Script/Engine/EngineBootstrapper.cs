using UnityEngine;
using UnityEngine.SceneManagement;

public class EngineBootstrapper : ScriptableObject
{
    private static string _pathDependenciesPrefab = "Prefabs/Dependencies";
    private static string _pathBootstrapperPrefab = "Prefabs/Bootstrapper";
    
    //Can be removed, safety for load in specific scene
    private const string SceneLoader = "Bootstrapper";
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Initialization()
    {
        if (SceneManager.GetActiveScene().name != SceneLoader) return;
        
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
