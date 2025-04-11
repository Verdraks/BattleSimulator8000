public class InputBaker : BakerData
{
    protected override void Bake()
    {
        var inputData = new InputData();
        
        querySystem.AddData<InputData>(ID,inputData);
    }
}