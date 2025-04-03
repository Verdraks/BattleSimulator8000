public class HookerRotation : HookerData<RotationData>
{
    protected override void Awake()
    {
        base.Awake();
        Data.Transform = gameObject.transform;
    }
}