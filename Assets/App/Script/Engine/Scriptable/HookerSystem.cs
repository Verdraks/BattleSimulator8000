using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class HookerSystem : MonoBehaviour
{
    [Title("References")] 
    [SerializeField] private SystemScriptableObject systemScriptableObject;

    public void Update()
    {
        systemScriptableObject.Update();
    }
}
