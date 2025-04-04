using Sirenix.OdinInspector;
using UnityEngine;

public class BoatController : ScriptableObjectSystem
{
     [Title("References")]
     [SerializeField] private QuerySystem querySystem;

     public override void Update()
     {
          if (querySystem.GetData<BoatData>(out var datas))
          {
               foreach (var data in datas)
               {
                    data.Agent.SetDestination()
               }
          }
     }
}