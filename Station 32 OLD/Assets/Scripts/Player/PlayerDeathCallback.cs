using UnityEngine;
using System;
using System.Collections;

public class PlayerDeathCallback : MonoBehaviour
{
    /// <summary>
    /// Return Callback That Will Determine Death Delay.
    /// </summary>
    public static Action<float> ReturnCallback = (time) => MostCallbackTime = time;

    public static event Action ReturnDeathCallback;

    private static float MostCallbackTime;

    private void Awake()
    {
        Player.OnPlayerDeath += OnPlayerDieInitialize;
    }

    private void OnPlayerDieInitialize()
    {
        StartCoroutine(StartOnDeathCallbackCoroutine());
    }

    private IEnumerator StartOnDeathCallbackCoroutine()
    {
        yield return null;

        StartCoroutine(OnDeathCallback(MostCallbackTime));
    }

    private IEnumerator OnDeathCallback(float time)
    {
        yield return new WaitForSeconds(time);
        ReturnDeathCallback?.Invoke();
    }

    private void OnDestroy()
    {
        Player.OnPlayerDeath -= OnPlayerDieInitialize;
    }
}
