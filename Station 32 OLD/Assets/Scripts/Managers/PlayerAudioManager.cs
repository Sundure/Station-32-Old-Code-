using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    private static AudioSource PlayerAudioSource;

    private void Awake()
    {
        PlayerAudioSource = GetComponent<AudioSource>();
    }

    public static void Play(AudioClip clip) //TODO Add Multiply Play Support
    {
        if (PlayerAudioSource == null)
            return;

        PlayerAudioSource.volume = 1;

        PlayerAudioSource.clip = clip;
        PlayerAudioSource.Play();
    }

    public static void Play(AudioClip clip, float volume)
    {
        if (PlayerAudioSource == null)
            return;

        PlayerAudioSource.volume = volume;

        PlayerAudioSource.clip = clip;
        PlayerAudioSource.Play();
    }

    public static void PlayOneShoot(AudioClip clip)
    {
        if (PlayerAudioSource == null)
            return;

        PlayerAudioSource.volume = 1;

        PlayerAudioSource.PlayOneShot(clip);
    }
    public static void PlayOneShoot(AudioClip clip, float volume)
    {
        if (PlayerAudioSource == null)
            return;

        PlayerAudioSource.volume = volume;

        PlayerAudioSource.PlayOneShot(clip);
    }

    public static void ChangeAudioListinerVolume(float volume) => AudioListener.volume = volume;
}
