using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField] private RawImage _rendererImage;

    [SerializeField] private VideoClip _videoClip;
    public VideoClip VideoClip { get { return _videoClip; } }

    [SerializeField] private VideoPlayer _videoPlayer;

    [SerializeField] private bool _playOnCreate;
    [SerializeField] private bool _prepareOnCreate;

    private void Awake()
    {
        RenderTexture _rendererTexture = new(1920, 1080, 0);

        _videoPlayer.targetTexture = _rendererTexture;

        _videoPlayer.renderMode = VideoRenderMode.RenderTexture;

        _rendererImage.texture = _rendererTexture;

        if (_prepareOnCreate)
            Prepare();

        if (_playOnCreate)
        {
            _videoPlayer.Play();
            return;
        }

        _videoPlayer.Stop();
    }

    private void Start()
    {
        _videoPlayer.SetDirectAudioVolume(0, GameSettingsManager.Instance.GameSettings.MainVolume);
    }

    public void Prepare()
    {
        _videoPlayer.clip = VideoClip;
        _videoPlayer.Prepare();
    }

    public void Play()
    {
        _videoPlayer.Play();
    }

    public void Pause()
    {
        _videoPlayer.Pause();
    }

    public void Stop()
    {
        _videoPlayer.Stop();
    }
}
