using System;
using UnityEngine;

public class ExitDoor : Interacted
{
    public static event Action OnPlayerWin;

    private bool _open;

    [SerializeField] private GameObject _doorLight;

    private void Awake()
    {
        DoorTimer.TimeOver += OpenDoor;
    }

    private void OpenDoor()
    {
        _open = true;

        _doorLight.SetActive(true);
    }

    public override bool InteractCondition()
    {
        if (_open)
        {
            return true;
        }
        return false;
    }

    protected override void Use()
    {
        PlayerWin();
    }

    public void PlayerWin()
    {
        OnPlayerWin?.Invoke();
    }

    private void OnDestroy()
    {
        DoorTimer.TimeOver -= OpenDoor;
    }
}
