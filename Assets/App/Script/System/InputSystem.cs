using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputSystem", menuName = "SO/System/InputSystem")]
public class InputSystem : ScriptableObjectSystem
{
    [Title("Reference")]
    [SerializeField] private QuerySystem querySystem;

    private InputAction_Controller _inputActionController;
    
    public override void Enable()
    {
        base.Enable();
        _inputActionController = new InputAction_Controller();

        _inputActionController.Controller.Move.performed += Move;
        _inputActionController.Controller.Move.canceled += Move;
        
        _inputActionController.Controller.Zoom.performed += Zoom;
        _inputActionController.Controller.Zoom.canceled += Zoom;
        
        _inputActionController.Enable();
    }

    private void Move(InputAction.CallbackContext callbackContext)
    {
        if (querySystem.GetUniqueData<InputData>(out var data))
        {
            data.InputMove = callbackContext.ReadValue<Vector2>();
        }
    }
    
    
    private void Zoom(InputAction.CallbackContext callbackContext)
    {
        if (querySystem.GetUniqueData<InputData>(out var data))
        {
            data.InputZoom = callbackContext.ReadValue<float>();
        }
    }

    public override void Disable()
    {
        _inputActionController.Disable();
        
        _inputActionController.Controller.Move.performed -= Move;
        _inputActionController.Controller.Move.canceled -= Move;
        
        _inputActionController.Controller.Zoom.performed -= Zoom;
        _inputActionController.Controller.Zoom.canceled -= Zoom;
        
        _inputActionController = null;
        base.Disable();
    }
}