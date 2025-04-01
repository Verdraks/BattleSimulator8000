using UnityEngine;
public class HookerBoatData : HookerData<BoatData>
{
    [Header("References")]
    [SerializeField] private Transform boatTransform;

    protected override void Awake()
    {
        base.Awake();
        Data.boat = boatTransform;
    }
}