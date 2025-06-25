using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class AudioSettings : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    [SerializeField] private AudioMixerManager.AudioMixerType _audioMixerType;

    private void Awake()
    {
        EventTrigger eventTrigger = GetComponent<EventTrigger>();

        EventTrigger.Entry entry = new()
        {
            eventID = EventTriggerType.PointerUp
        };

        entry.callback.AddListener(OnPointerUp);
        eventTrigger.triggers.Add(entry);

        _slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void Start()
    {
        switch (_audioMixerType)
        {
            case (AudioMixerManager.AudioMixerType.Main):
                AudioMixerManager.ChangeVolume(GameSettingsManager.Instance.GameSettings.MainVolume, _audioMixerType);
                _slider.value = GameSettingsManager.Instance.GameSettings.MainVolume;
                break;
            case (AudioMixerManager.AudioMixerType.Sound):
                AudioMixerManager.ChangeVolume(GameSettingsManager.Instance.GameSettings.SoundVolume, _audioMixerType);
                _slider.value = GameSettingsManager.Instance.GameSettings.SoundVolume;
                break;
        }

    }

    private void OnSliderValueChanged(float value)
    {
        float volume01 = value / _slider.maxValue;

        AudioMixerManager.ChangeVolume(volume01, _audioMixerType);

        switch (_audioMixerType)
        {
            case (AudioMixerManager.AudioMixerType.Main):
                GameSettingsManager.Instance.GameSettings.MainVolume = volume01;
                break;
            case (AudioMixerManager.AudioMixerType.Sound):
                GameSettingsManager.Instance.GameSettings.SoundVolume = volume01;
                break;
        }
    }

    public void OnPointerUp(BaseEventData _) // Save Settings On Pointer Up
    {
        GameSettingsManager.Instance.SaveSettings(GameSettingsManager.Instance.GameSettings);
    }
}
