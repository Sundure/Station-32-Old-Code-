using System.Collections;
using UnityEngine;

public class AudioScreamerManager : MonoBehaviour
{
    [Header("Transform")]
    [SerializeField] private Transform _enemy;
    [SerializeField] private Transform _player;

    [Header("Audio")]
    [SerializeField] private AudioClip[] _audioClips;

    [SerializeField] private float _audioVolume;

    [Header("Values")]
    [SerializeField] private float _trigerDistance;

    [SerializeField] private float _audioCooldown;

    private void Update()
    {
        float distance = Vector3.Distance(_player.position, _enemy.position);

        if (distance <= _trigerDistance)
        {
            PlayerAudioManager.PlayOneShoot(_audioClips[Random.Range(0, _audioClips.Length)], _audioVolume);

            enabled = false;

            StartCoroutine(TriggerCooldown());
        }
    }

    private IEnumerator TriggerCooldown()
    {
        yield return new WaitForSeconds(_audioCooldown);

        enabled = true;
    }
}
