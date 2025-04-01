using UnityEngine;
[CreateAssetMenu(fileName = "NavigationSystem", menuName = "SO/Systems/Navigation System")]
public class NavigationSystem : SystemScriptableObject
{
    private const float Speed = 10f;

    public override void Update()
    {
        if (TargetData.dataQueryReadOnly.Count > 0)
        {
            if (!TargetData.GetSingleton(out TargetData targetData)) return;
            foreach (var boatData in BoatData.dataQueryReadOnly)
            {
                boatData.boat.transform.position += (targetData.target.transform.position - boatData.boat.transform.position).normalized * (Speed * Time.deltaTime);
            }
        }
    }
}