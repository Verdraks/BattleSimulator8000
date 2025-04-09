using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using IEnumerator = System.Collections.IEnumerator;

public static class Utils
{
    
    #region IENUMERABLE
    public static T GetRandom<T>(this IEnumerable<T> elems)
    {
        if (!elems.Any())
        {
            Debug.LogError("Try to get random elem from empty IEnumerable");
        }
        return elems.ElementAt(UnityEngine.Random.Range(0, elems.Count()));
    }
    #endregion

    #region COROUTINE

    public static void Delay(this MonoBehaviour hook, Action ev, YieldInstruction yieldInstruction)
    {
        IEnumerator DelayCoroutine()
        {
            yield return yieldInstruction;
            ev?.Invoke();
        }

        hook.StartCoroutine(DelayCoroutine());
    }
    
    public static void Delay(this MonoBehaviour hook, Action ev, float time)
    {
        IEnumerator DelayCoroutine()
        {
            yield return new WaitForSeconds(time);
            ev?.Invoke();
        }

        hook.StartCoroutine(DelayCoroutine());
    }
    
    public static void Delay(this MonoBehaviour hook, Action ev, IEnumerator coroutine)
    {
        IEnumerator DelayCoroutine()
        {
            yield return coroutine;
            ev?.Invoke();
        }

        hook.StartCoroutine(DelayCoroutine());
    }
    
    #endregion

    #region Task

    public static async void AwaitTask(int delayMs,Action ev)
    {
        try
        {
            await Task.Delay(delayMs);
            ev?.Invoke();
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }
    }

    #endregion
    
}