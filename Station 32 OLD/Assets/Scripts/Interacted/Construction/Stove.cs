using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Stove : Interacted
{
    [SerializeField] private WarmZone _warmZone; // Stove Component Work Only With "WarmZone" Component

    [Header("Properties")]
    public bool Burn { get; private set; }

    private float _burningCoefficient;

    private const float MAX_BURNING_COEFFICIENT = 3;

    public float WarmStrength { get; private set; }

    [FormerlySerializedAs("_warmStrenghtPerFuel")] [SerializeField] private float _warmStrengthPerFuel = 2;

    [SerializeField] private float _fuelBurningTime = 30;

    private float _burningTime;

    private readonly List<Fuel> _burningFuel = new();

    [Header("Visual Effects")]

    [Space]
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _particleCountMultiplier = 10;

    [Space]
    [SerializeField] private Light _light;

    [SerializeField] private float _lightPower = 3;

    [Header("Other")]

    [SerializeField] private AudioSource _audioSource;
    private class Fuel
    {
        public Fuel(float burningTime)
        {
            BurningTime = burningTime;
            BurningTimeLeft = burningTime;
        }

        public float BurningTime;
        public float BurningTimeLeft;

        public float IgnitesTime => BurningTime / 4;
        public float PeakTime => BurningTime / 2;
        public float BurningOutTime => BurningTime / 4;
    }

    private void Awake()
    {
        _audioSource.loop = true;

        _light.intensity = 0;

        DisableStove();
    }

    private void Update()
    {
        if (_burningTime <= 0)
        {
            _burningTime = 0;

            DisableStove();

            return;
        }

        _burningCoefficient = 0;

        for (int i = 0; i < _burningFuel.Count; i++)
        {
            Fuel fuel = _burningFuel[i];

            if (fuel.BurningTimeLeft > fuel.PeakTime + fuel.BurningOutTime)
            {
                float timeRange = fuel.BurningTimeLeft - fuel.PeakTime - fuel.BurningOutTime;

                _burningCoefficient += Mathf.Abs((timeRange / fuel.IgnitesTime) -1);

                fuel.BurningTimeLeft -= Time.deltaTime / _burningFuel.Count;

            }
            else if (fuel.BurningTimeLeft >= fuel.IgnitesTime && fuel.BurningTimeLeft <= fuel.PeakTime + fuel.BurningOutTime)
            {
                _burningCoefficient ++;

                fuel.BurningTimeLeft -= Time.deltaTime / _burningFuel.Count;
            }
            else if (fuel.BurningTimeLeft < fuel.PeakTime + fuel.IgnitesTime)
            {
                _burningCoefficient += Mathf.Abs(fuel.BurningTimeLeft / fuel.BurningOutTime);

                fuel.BurningTimeLeft -= Time.deltaTime / _burningFuel.Count;
            }

            if (fuel.BurningTimeLeft <= 0)
            {
                _burningFuel.Remove(fuel);
                i--;
            }
        }

        WarmStrength = _warmStrengthPerFuel * _burningCoefficient;

        _audioSource.volume = _burningCoefficient / MAX_BURNING_COEFFICIENT;

        ParticleSystem.MainModule mainModule = _particleSystem.main;
        mainModule.simulationSpeed = _burningCoefficient;

        ParticleSystem.EmissionModule emissionModule = _particleSystem.emission;
        emissionModule.rateOverTime = _burningCoefficient * _particleCountMultiplier;

        _light.intensity = (_burningCoefficient / MAX_BURNING_COEFFICIENT) * _lightPower;

        _burningTime -= Time.deltaTime;
    }

    private void DisableStove()
    {
        Burn = false;
        enabled = false;

        _warmZone.enabled = false;
        _light.enabled = false;

        _audioSource.Stop();
        _particleSystem.Stop();
    }

    private void EnableStove()
    {
        Burn = true;
        enabled = true;

        _warmZone.enabled = true;
        _light.enabled = true;

        _audioSource.Play();
        _particleSystem.Play();
    }

    public override bool InteractCondition()
    {
        if (Player.Instance.FuelCount > 0)
        {
            return true;
        }
        return false;
    }

    protected override void Use()
    {
        Player.Instance.FuelCount--;
        ItemsUI.Instance.ChangeFuelCount();

        _burningTime += _fuelBurningTime;
        _burningFuel.Add(new(_fuelBurningTime));

        if (enabled == false)
            EnableStove();
    }
}