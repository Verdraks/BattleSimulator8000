using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraControllerSystem", menuName = "SO/System/CameraControllerSystem")]
public class CameraControllerSystem : ScriptableObjectSystem
{

    [Title("Settings")] 
    [SerializeField] private float speed;
    
    [Title("Reference")]
    [SerializeField] private QuerySystem querySystem;
    
    public override void Update()
    {
        base.Update();
        if (querySystem.GetUniqueData<InputData>(out var inputData))
        {
            if (querySystem.GetData<CameraTag, TransformData>(out var datas))
            {
                foreach (var data in datas)
                {
                    data.Item2.Transform.position += new Vector3(inputData.InputMove.x,0,inputData.InputMove.y) * (Time.deltaTime * speed);
                }
            }
        }
    }
}