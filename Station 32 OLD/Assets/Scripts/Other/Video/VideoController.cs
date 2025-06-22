using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField] private RawImage _rendererImage;

    [SerializeField] private VideoClip _videoClip;
    public VideoClip VideoClip { get { return _videoClip; } }

    private RenderTexture _rendererTexture;

    [SerializeField] private VideoPlayer _videoPlayer;

    [SerializeField] private bool _playOnCreate;

    private void Awake()
    {
        _rendererTexture = new(1920, 1080, 0);
        _videoPlayer.targetTexture = _rendererTexture;

        _videoPlayer.clip = VideoClip;
        _videoPlayer.Prepare();

        _videoPlayer.renderMode = VideoRenderMode.RenderTexture;

        _rendererImage.texture = _rendererTexture;

        if (_playOnCreate)
        {
            _videoPlayer.Play();
            return;
        }

        _videoPlayer.Stop();
    }

    public void Play()
    {
        gameObject.SetActive(true);

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
