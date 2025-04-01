using UnityEngine;
public class HookerTargetData : HookerData<TargetData>
{
    [Header("References")]
    [SerializeField] private Transform targetTransform;

    protected override void Awake()
    {
        base.Awake();
        Data.target = targetTransform;
    }
}