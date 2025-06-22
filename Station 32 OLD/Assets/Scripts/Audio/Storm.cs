using UnityEngine;

public class Storm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _audioClip;

    private void Awake()
    {
        _audioSource.clip = _audioClip;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    private void Update()
    {
        DoorTimer timer = DoorTimer.Instance;

        _audioSource.volume = timer.TimeLeft / timer.TotalTime;
    }
}
