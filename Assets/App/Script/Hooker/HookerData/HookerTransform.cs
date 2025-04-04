public class HookerTransform : HookerData<TransformData>
{
    protected override void Awake()
    {
        base.Awake();
        Data.Transform = transform;
    }
}