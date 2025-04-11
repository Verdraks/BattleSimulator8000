using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraControllerSystem", menuName = "SO/Systems/CameraControllerSystem")]
public class CameraControllerSystem : ScriptableObjectSystem
{

    [Title("Settings")] 
    [SerializeField] private float speed;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float mouseSensitivity;
    
    [Title("References")]
    [SerializeField] private QuerySystem querySystem;
    
    public override void Execute()
    {
        if (querySystem.GetUniqueData<CameraData>(out var cameraData))
        {
            if (querySystem.GetUniqueData<InputData>(out var inputData))
            {
                LookCamera(cameraData, inputData);
                MoveCamera(cameraData, inputData);
            }
        }
    }

    
    private void MoveCamera(CameraData cameraData, InputData inputData)
    {
        Transform transformCamera = cameraData.Camera.transform;
        
        Vector3 move = transformCamera.right * inputData.DirectionMovement.x + transformCamera.forward * inputData.DirectionMovement.y;
        
        transformCamera.position += move * ((inputData.Sprint ? speed * speedMultiplier : speed) * Time.deltaTime);
    }

    private void LookCamera(CameraData cameraData, InputData inputData)
    {
        cameraData.Yaw += inputData.DirectionRotation.x * mouseSensitivity;;
        cameraData.Pitch -= inputData.DirectionRotation.y * mouseSensitivity;;
        cameraData.Pitch = Mathf.Clamp(cameraData.Pitch, -90f, 90f);

        cameraData.Camera.transform.rotation = Quaternion.Euler(cameraData.Pitch, cameraData.Yaw, 0f);
    }
}