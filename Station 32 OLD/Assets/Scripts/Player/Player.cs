using System;
using UnityEngine;

[RequireComponent(typeof(PlayerDeathCallback))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [Header("Player Stats")]

    public bool Alive { get; private set; } = true;

    public float Health;
    public float MaxHealth = 200;

    [HideInInspector] public float Temperature;

    public float DefaultTemperature = -1;

    [Header("Inventory")] //TODO Make Full Inventory System

    public bool Screwdriver;
    public bool Key;

    public int FuelCount;
    public int FlareCount;

    [Header("Components")]

    [SerializeField] private UsableFlare _flare;
    public UsableFlare Flare { get { return _flare; } }

    // Other

    public static event Action OnPlayerDeath;
    public static Action ReturnDeathCallback { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Health = MaxHealth;

        tag = "Player";

        ReturnDeathCallback += OnDeathCallback;
    }

    private void Update()
    {
        if (Health > 0)
        {
            Health += Temperature * Time.deltaTime;
            Health = Mathf.Clamp(Health, 0, MaxHealth);

            Temperature = DefaultTemperature;
        }
        else
            Die();
    }

    /// <summary>
    /// If You Want To Death Delay Read PlayerDeathCallback.cs
    /// </summary>
    public void Die()
    {
        if (Alive == false) return;

        Alive = false;
        PlayerDeathCallback.ReturnDeathCallback += OnDeathCallback;

        OnPlayerDeath?.Invoke();
    }

    private void OnDeathCallback()
    {
        SceneManager.LoadScene(SceneList.MainMenu);
    }

    private void OnDestroy()
    {
        PlayerDeathCallback.ReturnDeathCallback -= OnDeathCallback;
    }
}
