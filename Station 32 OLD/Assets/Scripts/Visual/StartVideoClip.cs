using UnityEngine;

public class StartVideoClip : MonoBehaviour
{
    [SerializeField] private VideoClipDestroyer _videoClipDestroyer;
    [SerializeField] private VideoController _videoController;

    private void Start()
    {
        _videoClipDestroyer.OnDestroyed += OnVideoClipDestroyed;

        PauseManager.Instance.ChangePauseManagerStates(false,true);
    }

    private void OnVideoClipDestroyed()
    {
        PauseManager.Instance.ChangePauseManagerStates(true, false);
        _videoController.Stop();
    }

    private void OnDestroy()
    {
        _videoClipDestroyer.OnDestroyed -= OnVideoClipDestroyed;
    }
}
