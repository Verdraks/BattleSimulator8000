using UnityEngine;

/// <summary>
/// Scriptable Object with overload unity events in runtime
/// </summary>
public abstract class ScriptableObjectDynamic : ScriptableObject
{
    private bool _initialized;

    private void Awake()
    {
        if (!_initialized && Application.isPlaying) Enable();
    }

    private void OnDestroy()
    {
        if (_initialized && Application.isPlaying) Disable();
    }

    public virtual void Enable()
    {
        _initialized = true;
    }

    public virtual void Disable()
    {
        _initialized = false;
    }
}
