using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private RawImage _rendererImage;

    [SerializeField] private VideoClip _videoClip;
    public VideoClip VideoClip { get { return _videoClip; } }

    [SerializeField] private VideoPlayer _videoPlayer;

    [Header("General Settings")]
    [SerializeField] private bool _playOnCreate;
    [SerializeField] private bool _prepareOnCreate;
    [SerializeField] private bool _destroyOnClipEnded;
    [SerializeField] private bool _canSkip;
    [SerializeField] private bool _renderVideoWhenPlaying;

    private RenderTexture _rendererTexture;

    public event Action OnDestroyed;
    public event Action OnSkiped;

    private readonly float _skipMinimalTime = 0.5f;

    private bool _canSkipDelayed;

    private void Awake()
    {
        _rendererTexture = new(1920, 1080, 0)
        {
            name = _videoClip.name + " Render Texture"
        };

        _rendererImage.texture = _rendererTexture;

        if (_renderVideoWhenPlaying)
            _rendererImage.enabled = false;

        enabled = false;

        if (_prepareOnCreate)
            Prepare();

        if (_playOnCreate)
            Play();
    }

    private void Start()
    {
        _videoPlayer.SetDirectAudioVolume(0, GameSettingsManager.Instance.GameSettings.MainVolume);
    }

    private void Update()
    {
        if (Input.anyKeyDown && _canSkip && _canSkipDelayed)
        {
            OnSkiped?.Invoke();

            OnDestroyed?.Invoke();

            Stop();
            Destroy(gameObject);
        }
    }

    private IEnumerator WaitSkipDelay(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        _canSkipDelayed = true;
    }

    private IEnumerator DestroyVideoClipDelay(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        OnDestroyed?.Invoke();

        Stop();
        Destroy(gameObject);
    }

    public void Prepare()
    {
        _videoPlayer.targetTexture = _rendererTexture;

        _videoPlayer.renderMode = VideoRenderMode.RenderTexture;

        _videoPlayer.clip = VideoClip;
        _videoPlayer.Prepare();
    }

    public void Play()
    {
        _videoPlayer.Play();

        _rendererImage.enabled = true;
        enabled = true;

        if (_canSkip)
            StartCoroutine(WaitSkipDelay(_skipMinimalTime));

        if (_destroyOnClipEnded)
            StartCoroutine(DestroyVideoClipDelay((float)_videoClip.length));
    }

    public void Pause()
    {
        if (_renderVideoWhenPlaying)
            _rendererImage.enabled = false;
        
        _videoPlayer.Pause();

        enabled = false;
    }

    public void Stop()
    {
        if (_renderVideoWhenPlaying)
            _rendererImage.enabled = false;
        
        _videoPlayer.Stop();

        enabled = false;
    }
}
