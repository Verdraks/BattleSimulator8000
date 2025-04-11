using Sirenix.OdinInspector;
using UnityEngine;

public class CameraBaker : BakerData
{
    
    [Title("Settings")]
    [SerializeField] private Camera cam;
    
    protected override void Bake()
    {
        var cameraData = new CameraData()
        {
            Camera = cam
        };
        
        querySystem.AddData<CameraData>(ID,cameraData);
    }
}