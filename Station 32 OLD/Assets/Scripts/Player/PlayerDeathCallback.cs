using UnityEngine;
using System;
using System.Collections;

public class PlayerDeathCallback : MonoBehaviour
{
    /// <summary>
    /// Return Callback That Will Determine Death Delay.
    /// </summary>
    public static Action<float> ReturnScaledCallback { get; private set; } = (time) =>
    {
        if (time > _mostScaledCallbackTime)
            _mostScaledCallbackTime = time;
    };

    /// <summary>
    /// Return Callback That Not Scaled At Delta Time.
    /// </summary>
    public static Action<float> ReturnUnscaledCallback { get; private set; } = (time) =>
    {
        if (time > _mostScaledCallbackTime)
            _mostUnscaledCallbackTime = time;
    };

    /// <summary>
    /// Stop Scaled At Time Delta Time Coroutine And Return Callback If Player Dead.
    /// </summary>
    public static Action StopScaledCoroutine { get; private set; } = () =>
    {
        _instance.StopCoroutine(_scaledCoroutine);
        _playingScaledCoroutine = false;
        _instance.CheckPlayingCoroutine();
    };

    /// <summary>
    /// Stop Unscaled At Time Delta Time Coroutine And Return Callback If Player Dead.
    /// </summary>
    public static Action StopUnscaledCoroutine { get; private set; } = () =>
    {
        _instance.StopCoroutine(_unscaledCoroutine);
        _playingUnscaledCoroutine = false;
        _instance.CheckPlayingCoroutine();
    };

    public static event Action ReturnDeathCallback;

    private static float _mostScaledCallbackTime;
    private static float _mostUnscaledCallbackTime;

    private static bool _playingScaledCoroutine;
    private static bool _playingUnscaledCoroutine;

    private static Coroutine _scaledCoroutine;
    private static Coroutine _unscaledCoroutine;

    private static PlayerDeathCallback _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }

        Player.OnPlayerDeath += OnPlayerDieInitialize;

        _instance = this;
    }

    private void OnPlayerDieInitialize()
    {
        StartCoroutine(StartOnDeathCallbackCoroutine());
    }

    private IEnumerator StartOnDeathCallbackCoroutine()
    {
        yield return null;

        _scaledCoroutine = StartCoroutine(ScaledOnDeathCallback(_mostScaledCallbackTime));
        _unscaledCoroutine = StartCoroutine(UnscaledOnDeathCallback(_mostUnscaledCallbackTime));
    }

    private IEnumerator ScaledOnDeathCallback(float time)
    {
        if (time > 0)
        {
            _playingScaledCoroutine = true;
            yield return new WaitForSeconds(time);
            _playingScaledCoroutine = false;
            ReturnDeathCallback?.Invoke();
        }
    }

    private IEnumerator UnscaledOnDeathCallback(float time)
    {
        if (time > 0)
        {
            _playingUnscaledCoroutine = true;
            yield return new WaitForSecondsRealtime(time);
            _playingUnscaledCoroutine = false;
            ReturnDeathCallback?.Invoke();
        }
    }

    private void CheckPlayingCoroutine() // Return Callback If No Coroutine Is Playing
    {
        if (Player.Instance.Alive)
            return;

        if (_playingScaledCoroutine == false && _playingUnscaledCoroutine == false)
        {
            ReturnDeathCallback?.Invoke();
        }
    }

    private void OnDestroy()
    {
        _instance = null;
        Player.OnPlayerDeath -= OnPlayerDieInitialize;
    }
}
