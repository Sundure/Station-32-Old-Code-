using System;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool Pause { get; private set; }

    public static event Action<bool> OnPauseChange;

    private static PauseManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        ExitDoor.OnPlayerWin += Disable;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool pause = !Pause;

            ChangePause(pause);
        }
    }

    public static void ChangePause(bool pause)
    {
        Pause = pause;

        OnPauseChange?.Invoke(Pause);

        Time.timeScale = Pause ? 0 : 1;
    }

    private void Disable()
    {
        if (Pause)
        {
            Pause = false;

            OnPauseChange?.Invoke(Pause);

            Time.timeScale = 1;
        }

        enabled = false;
    }

    private void OnDestroy()
    {
        ExitDoor.OnPlayerWin -= Disable;
    }
}
