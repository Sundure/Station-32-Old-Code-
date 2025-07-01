using UnityEngine;

public class DeathVideoClip : MonoBehaviour
{
    [SerializeField] private VideoController _videoController;

    private void Awake()
    {
        Player.OnPlayerDeath += ActivateClip;
        _videoController.OnSkiped += OnClipSkiped;
    }

    private void ActivateClip()
    {
        _videoController.Prepare();
        _videoController.Play();

        PlayerDeathCallback.ReturnUnscaledCallback?.Invoke((float)_videoController.VideoClip.length);
        PauseManager.Instance.ChangePauseManagerStates(false, true);
    }

    private void OnClipSkiped()
    {
        PlayerDeathCallback.StopUnscaledCoroutine?.Invoke();
    }

    private void OnDestroy()
    {
        Player.OnPlayerDeath -= ActivateClip;
        _videoController.OnSkiped -= OnClipSkiped;
    }
}
