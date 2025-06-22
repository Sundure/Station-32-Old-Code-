using UnityEngine;
using UnityEngine.UI;

public class GlobalAudioSettings : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void Awake()
    {
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        PlayerAudioManager.ChangeAudioListinerVolume(value / _slider.maxValue);
    }
}
