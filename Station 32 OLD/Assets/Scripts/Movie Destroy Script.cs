using UnityEngine;
using UnityEngine.Video;

public class MovieDestroyScript : MonoBehaviour
{
    [SerializeField] private float _movieTime;
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private bool _canSkip;
    [SerializeField] private bool _isStart;
    [SerializeField] private bool _isDeathMovie;
    [SerializeField] private AudioSource _stormAudioSource;
    [SerializeField] private AudioSource _flareAudioSource;
    [SerializeField] private GameObject _blackScreenEffect;
    [SerializeField] private GameObject _blackScreenStartEffect;
    [SerializeField] private CameraController _cameraScript;
    [SerializeField] private VideoClip _clip;

    private void Start()
    {
        _videoPlayer.SetDirectAudioVolume(0, 0.5f);
        _stormAudioSource.enabled = false;
        _cameraScript.SensX = 0;
        _cameraScript.SensY = 0;
        if (_isDeathMovie == true)
        {
            _flareAudioSource.enabled = false;
            _videoPlayer.clip = _clip;
        }
        if (_isStart == true)
        {
            _blackScreenStartEffect.SetActive(true);
        }
    }
    private void Update()
    {
        _movieTime -= Time.deltaTime;
        if (_movieTime < 0)
        {
            _stormAudioSource.enabled = true;
            if (_isStart == true)
            {
                _blackScreenEffect.SetActive(true);
            }
            _cameraScript.SensX = 150;
            _cameraScript.SensY = 150;
            _videoPlayer.SetDirectAudioVolume(0, 0f);
            if (_isStart == true)
            {
                _blackScreenStartEffect.SetActive(false);
            }
            Destroy(gameObject);
        }
        if (Input.anyKeyDown && _canSkip == true)
        {
            _stormAudioSource.enabled = true;
            _videoPlayer.SetDirectAudioVolume(0, 0f);
            _blackScreenEffect.SetActive(true);
            _cameraScript.SensX = 150;
            _cameraScript.SensY = 150;
            if (_isStart == true)
            {
                _blackScreenStartEffect.SetActive(false);
            }
            Destroy(gameObject);
        }
    }
}
