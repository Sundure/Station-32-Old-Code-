using System;
using UnityEngine;

public class DoorTimer : MonoBehaviour
{
    public static DoorTimer Instance { get; private set; }

    public static event Action TimeOver;

    [SerializeField] private float _totalTime = 480;
    public float TotalTime { get { return _totalTime; } }

    [SerializeField] private float _timeLeft;
    public float TimeLeft { get { return _timeLeft; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _timeLeft = TotalTime;
    }

    private void Update()
    {
        _timeLeft -= Time.deltaTime;

        if (_timeLeft <= 0)
        {
            TimeOver?.Invoke();

            enabled = false;
        }
    }
}
