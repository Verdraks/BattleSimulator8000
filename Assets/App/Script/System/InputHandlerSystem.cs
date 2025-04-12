using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputHandlerSystem", menuName = "SO/Systems/InputHandlerSystem")]
public class InputHandlerSystem : ScriptableObjectSystem
{
    [Title("References")]
    [SerializeField] private QuerySystem querySystem;
    
    private InputActionMain _inputActionMain;

    public override void Enable()
    {
        _inputActionMain = new InputActionMain();
        
        _inputActionMain.Action.Destroy.performed += InputDestroy;
        _inputActionMain.Action.Destroy.canceled += InputDestroy;
        
        _inputActionMain.Action.Add.performed += InputAdd;
        _inputActionMain.Action.Add.canceled += InputAdd;
        
        _inputActionMain.Action.Validate.performed += InputValidate;
        
        _inputActionMain.Action.SwapIndex.performed += InputSwapIndex;
        
        _inputActionMain.Controller.Move.performed += InputMove;
        _inputActionMain.Controller.Move.canceled += InputMove;

        _inputActionMain.Controller.Look.performed += InputLook;
        _inputActionMain.Controller.Look.canceled += InputLook;

        _inputActionMain.Controller.Sprint.performed += InputSprint;
        _inputActionMain.Controller.Sprint.canceled += InputSprint;
        
        _inputActionMain.Enable();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        base.Enable();
    }

    private void InputSprint(InputAction.CallbackContext obj)
    {
        if (querySystem.GetUniqueData<InputData>(out var data))
        {
            data.Sprint = !data.Sprint;
        }
    }

    private void InputLook(InputAction.CallbackContext obj)
    {
        if (querySystem.GetUniqueData<InputData>(out var data))
        {
            data.DirectionRotation = obj.ReadValue<Vector2>();
        }
    }

    private void InputSwapIndex(InputAction.CallbackContext obj)
    {
        if (querySystem.GetUniqueData<InputData>(out var data))
        {
            data.IndexPrefabs += obj.ReadValue<int>();
        }
    }

    private void InputValidate(InputAction.CallbackContext obj)
    {
        if (querySystem.GetUniqueData<InputData>(out var data))
        {
            data.Validate = !data.Validate;
        }
    }

    private void InputMove(InputAction.CallbackContext obj)
    {
        if (querySystem.GetUniqueData<InputData>(out var data))
        {
            data.DirectionMovement = obj.ReadValue<Vector2>();
        }
    }

    private void InputAdd(InputAction.CallbackContext obj)
    {
        if (querySystem.GetUniqueData<InputData>(out var data))
        {
            data.InstantiateUnit = !data.InstantiateUnit;
        }
    }

    private void InputDestroy(InputAction.CallbackContext obj)
    {
        if (querySystem.GetUniqueData<InputData>(out var data))
        {
            data.DestroyUnit = !data.DestroyUnit;
        }
    }


    public override void Disable()
    {
     
        if (_inputActionMain == null) return;
        
        _inputActionMain.Action.Destroy.performed -=InputDestroy;
        _inputActionMain.Action.Destroy.canceled -= InputDestroy;
        
        _inputActionMain.Action.Add.performed -= InputAdd;
        _inputActionMain.Action.Add.canceled -= InputAdd;
        
        _inputActionMain.Action.Validate.performed -= InputValidate;
        
        _inputActionMain.Action.SwapIndex.performed -= InputSwapIndex;
        
        _inputActionMain.Controller.Move.performed -= InputMove;
        _inputActionMain.Controller.Move.canceled -= InputMove;
        
        _inputActionMain.Controller.Look.performed += InputLook;
        _inputActionMain.Controller.Look.canceled += InputLook;
        
        _inputActionMain.Disable();
        
        _inputActionMain = null;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        base.Disable();
    }
}