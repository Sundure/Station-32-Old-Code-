using UnityEngine;

public class StartVideoClip : MonoBehaviour
{
    [SerializeField] private VideoController _videoController;

    private void Start()
    {
        _videoController.OnDestroyed += OnVideoClipDestroyed;

        PauseManager.Instance.ChangePauseManagerStates(false,true);
    }

    private void OnVideoClipDestroyed()
    {
        PauseManager.Instance.ChangePauseManagerStates(true, false);
        _videoController.Stop();
    }

    private void OnDestroy()
    {
        _videoController.OnDestroyed -= OnVideoClipDestroyed;
    }
}
