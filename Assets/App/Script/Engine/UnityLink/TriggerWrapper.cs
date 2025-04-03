using Sirenix.OdinInspector;
using UnityEngine;

public abstract class TriggerWrapper : MonoBehaviour
{
    [Title("Settings")]
    [SerializeField] private TriggerType triggerType;
    
    private void OnTriggerEnter(Collider other)
    {
        if (triggerType == TriggerType.Enter) Trigger(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (triggerType == TriggerType.Exit) Trigger(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (triggerType == TriggerType.Stay) Trigger(other);
    }
    
    protected abstract void Trigger(Collider other);


    private enum TriggerType
    {
        Enter,
        Exit,
        Stay,
    }
    
}
