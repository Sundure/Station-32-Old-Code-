using UnityEngine;

public class ActivateOnPlayerDeathClip : MonoBehaviour
{
    [SerializeField] private VideoController _videoController;

    private void Awake()
    {
        Player.OnPlayerDeath += ShowDeathMovie;
    }

    public void ShowDeathMovie()
    {
        PlayerDeathCallback.ReturnCallback((float)_videoController.VideoClip.length);

        _videoController.Play();
    }

    private void OnDestroy()
    {
        Player.OnPlayerDeath -= ShowDeathMovie;
    }
}
