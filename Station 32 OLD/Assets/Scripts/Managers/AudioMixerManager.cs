using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _mainAudioMixer;
    [SerializeField] private AudioMixer _mainIgnoringPauseAudioMixer;
    [SerializeField] private AudioMixer _soundAudioMixer;

    public AudioMixerGroup MainAudioMixerGroup { get; private set; }
    public AudioMixerGroup MainIgnoringPauseGroup { get; private set; }
    public AudioMixerGroup SoundAudioMixerGroup { get; private set; }

    public static AudioMixerManager Instance { get; private set; }

    private const float MIN_VOLUME = -80f;

    public enum AudioMixerType
    {
        Main,
        Sound,
    }

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

        MainAudioMixerGroup = _mainAudioMixer.FindMatchingGroups("Master")[0];
        SoundAudioMixerGroup = _soundAudioMixer.FindMatchingGroups("Master")[0];
    }


    /// <summary>
    /// Changes Volume From 0 to 1.
    /// <br/>
    /// AudioMixer.SetFloat() Don't Work On Awake On Scene Create Moment, So Use Start Instead.
    /// </summary>
    /// <param name="volume"></param>
    /// <param name="mixerType"></param>
    public static void ChangeVolume(float volume, AudioMixerType mixerType)
    {
        float volumeDB;

        if (volume < 0.0001f) //Log10 Inf Protection
        {
            volumeDB = MIN_VOLUME;
        }
        else
            volumeDB = Mathf.Log10(volume);

        switch (mixerType)
        {
            case AudioMixerType.Main:
                Instance._mainAudioMixer.SetFloat("Volume", volumeDB * 20);

                volume = GameSettingsManager.Instance.GameSettings.MainVolume;

                if (volume < 0.0001f) //Log10 Inf Protection
                {
                    volumeDB = MIN_VOLUME;
                }
                else
                    volumeDB = Mathf.Log10(volume);

                Instance._mainIgnoringPauseAudioMixer.SetFloat("Volume", GameSettingsManager.Instance.GameSettings.MainVolume * 20);
                break;
            case AudioMixerType.Sound:
                Instance._soundAudioMixer.SetFloat("Volume", volumeDB * 20);
                break;
        }
    }
}
