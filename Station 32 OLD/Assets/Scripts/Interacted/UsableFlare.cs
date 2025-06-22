using UnityEngine;

public class UsableFlare : MonoBehaviour // This Class Named "UsableFlare" Because Name "Flare" Is Busy
{
    [Header("GameObjects")]

    [SerializeField] private GameObject _flare;

    [SerializeField] private GameObject _flareCap;

    [Header("Components Values")]

    [SerializeField] private float _lightRangeDivider = 3;
    [SerializeField] private float _lightIntensityDivider = 80;
    [SerializeField] private float _particleSimulationSpeedDivider = 28;
    [SerializeField] private float _audioVolumeDivider = 520;

    [Header("Flare Values")]

    [SerializeField] private float _flareBurnTime;
    [SerializeField] private float _maxFlareBurnTime = 140;

    [SerializeField] private float _flareIntensity = 100;
    private float CurrentFlareIntensity => _flareIntensity * (_flareBurnTime / _maxFlareBurnTime);

    private bool _burn;


    private float _currentUseCooldown;
    public float CurrentUseCooldown { get { return _currentUseCooldown; } }

    [SerializeField] private float _useCooldown = 1.5f; // Time Between Flare Usage


    [SerializeField] private float _useDelay; // How Long Need To Hold Left Button To Use Flare
    public float UseDelay { get { return _useDelay; } }


    private float _useDelayCharge;
    public float UseDelayCharge { get { return _useDelayCharge; } }

    [Header("Components")]

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _flareBurnAudioClip;

    [SerializeField] private Light _light;

    [SerializeField] private ParticleSystem _particleSystem;

    private void Awake()
    {
        _currentUseCooldown = _useCooldown;

        _audioSource.clip = _flareBurnAudioClip;
        _audioSource.loop = true;

        _light.enabled = false;
    }

    private void Update()
    {
        if (_currentUseCooldown == 0)
        {
            if (Input.GetKey(KeyCode.Mouse0) && Player.Instance.FlareCount > 0)
            {
                _useDelayCharge += Time.deltaTime;

                if (_useDelayCharge >= _useDelay)
                {
                    _useDelayCharge = 0;
                    _currentUseCooldown = _useCooldown;

                    Player.Instance.FlareCount--;
                    ItemsUI.Instance.ChangeFlareCount();

                    EnableFlare();
                }
            }
            else
            {
                _useDelayCharge -= Time.deltaTime;
                _useDelayCharge = Mathf.Clamp(_useDelayCharge, 0, _useDelay);
            }
        }
        else
        {
            _currentUseCooldown -= Time.deltaTime;
            _currentUseCooldown = Mathf.Clamp(_currentUseCooldown, 0, _useCooldown);
        }


        if (_burn)
        {
            _flareBurnTime -= Time.deltaTime;

            if (_flareBurnTime <= 0)
            {
                DisableFlare();
                return;
            }

            _light.intensity = CurrentFlareIntensity / _lightIntensityDivider;
            _light.range = CurrentFlareIntensity / _lightRangeDivider;
            _audioSource.volume = CurrentFlareIntensity / _audioVolumeDivider;

            var _particleSystemMain = _particleSystem.main;

            _particleSystemMain.simulationSpeed = (CurrentFlareIntensity + _flareIntensity / 10) / _particleSimulationSpeedDivider;
        }
    }

    private void EnableFlare()
    {
        _flareBurnTime = _maxFlareBurnTime;

        _flareCap.SetActive(false);

        _light.enabled = true;

        _audioSource.Play();
        _particleSystem.Play();

        _burn = true;
    }

    private void DisableFlare()
    {
        _flareCap.SetActive(true);

        if (Player.Instance.FlareCount <= 0)
            _flare.SetActive(false);

        _light.enabled = false;

        _audioSource.Stop();
        _particleSystem.Stop();

        _burn = false;
    }

}
