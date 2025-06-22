using UnityEngine;

public class PlayerDeathMovie : MonoBehaviour
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
