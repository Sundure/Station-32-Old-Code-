using UnityEngine;

public class PauseAudioOnPause : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        PauseManager.OnPauseChange += OnPauseChange;

        if (TryGetComponent(out _audioSource)) { }
        
        else
        {
            Debug.LogError($"AudioSource Not Found In \"{gameObject.name}\"");
        }
    }

    private void OnPauseChange(bool pauseStatus)
    {
        if (pauseStatus)
        {
            _audioSource.Pause();
        }
        else
        {
            _audioSource.UnPause();
        }
    }

    private void OnDestroy()
    {
        PauseManager.OnPauseChange -= OnPauseChange;
    }
}
