using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(fileName = "SpawningSystem", menuName = "SO/System/SpawningSystem")]
public class SpawningSystem : ScriptableObjectSystem
{
    [Title("Settings")] 
    [SerializeField] private int count;
    
    [Title("Reference")]
    [SerializeField] private GameObject prefab;

    public override void Enable()
    {
        base.Enable();
        var hookerSpawn = new GameObject($"{prefab.name} Hooker");

        int rootCount = Mathf.RoundToInt(Mathf.Pow(count,1.0f/3));
        
        for (int i = 0; i < count; i++)
        {
            Instantiate(prefab, new Vector3Int(i%rootCount, (i / rootCount) % rootCount, i/(rootCount*rootCount)),Quaternion.identity).transform.SetParent(hookerSpawn.transform);
        }
        
    }
}