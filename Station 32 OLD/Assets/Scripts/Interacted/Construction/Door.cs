using System.Collections;
using UnityEngine;

public class Door : Interacted
{
    [SerializeField] private Animator _animator;

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _doorOpenAudioClip;
    [SerializeField] private AudioClip _doorCloseAudioClip;

    [SerializeField] private DoorState _doorState = DoorState.Close;

    [SerializeField] private byte _doorKeyCode;

    private bool _canUse = true;

    //Animations Names
    private const string CLOSE_DOOR = "Close";
    private const string OPEN_DOOR = "Open";

    private enum DoorState
    {
        Open,
        Close,
        Locked
    }

    protected override void Use()
    {
        switch (_doorState)
        {
            case DoorState.Open:
                _doorState = DoorState.Close;

                _audioSource.clip = _doorCloseAudioClip;
                _audioSource.Play();

                _animator.Play(CLOSE_DOOR);

                StartCoroutine(WaitDelay(_animator.GetCurrentAnimatorStateInfo(0).length));
                break;
            case DoorState.Close:
                _doorState = DoorState.Open;

                _audioSource.clip = _doorOpenAudioClip;
                _audioSource.Play();

                _animator.Play(OPEN_DOOR);

                StartCoroutine(WaitDelay(_animator.GetCurrentAnimatorStateInfo(0).length));
                break;
            case DoorState.Locked:

                Debug.Log("Door is locked");

                _doorState = DoorState.Open;
                break;
        }
    }

    public override bool InteractCondition()
    {
        return _canUse;
    }

    private IEnumerator WaitDelay(float time)
    {
        _canUse = false;

        yield return new WaitForSeconds(time);

        _canUse = true;
    }
}
