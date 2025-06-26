using System;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool Pause { get; private set; }

    public static event Action<bool> OnPauseChange;

    public static PauseManager Instance;

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

        DontDestroyOnLoad(gameObject);

        ExitDoor.OnPlayerWin += Disable;
        SceneManager.OnSceneLoaded += OnSceneLoad;
        SceneManager.OnSceneFromListLoaded += OnSceneLoad;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePause(!Pause);
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
            ChangePause(false);
        }

        enabled = false;
    }

    public void ChangePauseManagerStates(bool enabled, bool pause)
    {
        this.enabled = enabled;
        ChangePause(pause);
    }

    private void OnSceneLoad()
    {
        ChangePauseManagerStates(true, false);
    }

    private void OnSceneLoad(SceneList scene)
    {
        if (scene == SceneList.MainMenu)
        {
            ChangePauseManagerStates(false, false);
            return;
        }
    }

    private void OnDestroy()
    {
        ExitDoor.OnPlayerWin -= Disable;
        SceneManager.OnSceneLoaded -= OnSceneLoad;
        SceneManager.OnSceneFromListLoaded -= OnSceneLoad;
    }
}
