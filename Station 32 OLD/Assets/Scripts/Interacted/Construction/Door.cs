using System.Collections;
using UnityEngine;

public class Door : Interacted
{
    [Header("Components")]
    [SerializeField] private Animator _animator;

    [SerializeField] private AudioSource _audioSource;

    [Header("Default Door Audio")]
    [SerializeField] private AudioClip _doorOpenAudioClip;
    [SerializeField] private AudioClip _doorCloseAudioClip;

    [Header("Door Key Audio")]
    [SerializeField] private AudioClip _doorKeyOpenAudioClip;
    [SerializeField] private AudioClip _doorKeyClosedAudioCLip;

    [Header("Other")]
    [SerializeField] private DoorState _doorState = DoorState.Closed;

    [SerializeField] private bool _canUse = true;

    //Animations Names
    private const string CLOSE_DOOR = "Close";
    private const string OPEN_DOOR = "Open";

    private enum DoorState
    {
        Open,
        Closed,
        Locked
    }

    protected override void Use()
    {
        switch (_doorState)
        {
            case DoorState.Open:
                _doorState = DoorState.Closed;

                _audioSource.clip = _doorCloseAudioClip;
                _audioSource.Play();

                _animator.Play(CLOSE_DOOR);

                StartCoroutine(WaitDelay(_animator.GetCurrentAnimatorStateInfo(0).length));
                break;
            case DoorState.Closed:
                _doorState = DoorState.Open;

                _audioSource.clip = _doorOpenAudioClip;
                _audioSource.Play();

                _animator.Play(OPEN_DOOR);

                StartCoroutine(WaitDelay(_animator.GetCurrentAnimatorStateInfo(0).length));
                break;
            case DoorState.Locked:
                if (Player.Instance.Key == true)
                {
                    _audioSource.clip = _doorKeyOpenAudioClip;
                    _audioSource.Play();

                    _doorState = DoorState.Closed;
                }
                else
                {
                    _audioSource.clip = _doorKeyClosedAudioCLip;
                    _audioSource.Play();
                }
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
