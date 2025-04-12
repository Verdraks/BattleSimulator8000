using UnityEngine;

/// <summary>
/// Scriptable Object with overload unity events in runtime
/// </summary>
public abstract class ScriptableObjectDynamic : ScriptableObject
{
    public bool initialized { get; private set; }

    private void Awake()
    {
        if (!initialized && Application.isPlaying) Enable();
    }

    private void OnDestroy()
    {
        if (initialized && Application.isPlaying) Disable();
    }

    public virtual void Enable()
    {
        initialized = true;
    }

    public virtual void Disable()
    {
        initialized = false;
    }
}
