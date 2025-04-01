using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class DataScriptableObject<T> : ScriptableObject where T : DataScriptableObject<T>
{
    protected static readonly List<T> DataQuery = new();
    public static IReadOnlyList<T> dataQueryReadOnly => DataQuery.AsReadOnly();

    public static bool GetSingleton(out T data)
    {
        data = null;
        if (DataQuery.Count > 1) return false;
        data = DataQuery[0];
        return true;
    }
    
    private bool _initialized = false;
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    protected static void PreInitialize()
    {
        DataQuery.Clear();
    }

    private void Awake()
    {
        if (!_initialized && Application.isPlaying) Enable();
    }

    private void OnEnable()
    {
        if (!_initialized && Application.isPlaying) Enable();
    }

    private void OnDisable()
    {
        if (_initialized && Application.isPlaying) Disable();
    }

    private void OnDestroy()
    {
        if (_initialized && Application.isPlaying) Disable();
    }

    protected virtual void Enable()
    {
        _initialized = true;
        DataQuery?.Add(this as T);
    }

    protected virtual void Disable()
    {
        _initialized = false;
        DataQuery?.Remove(this as T);
    }
}
