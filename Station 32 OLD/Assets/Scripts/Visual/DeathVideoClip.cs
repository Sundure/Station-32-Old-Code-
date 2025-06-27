using UnityEngine;

public class DeathVideoClip : MonoBehaviour
{
    [SerializeField] private VideoController _videoController;

    private void Start()
    {
        Player.OnPlayerDeath += ActivateClip;
    }

    private void ActivateClip()
    {
        _videoController.Play();
        PlayerDeathCallback.ReturnCallback?.Invoke((float)_videoController.VideoClip.length);
        PauseManager.Instance.ChangePauseManagerStates(false,true);
    }

    private void OnDestroy()
    {
        Player.OnPlayerDeath -= ActivateClip;
    }
}
