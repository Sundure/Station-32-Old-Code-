using UnityEngine;
using UnityEngine.UI;

public class FlareFillSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    [SerializeField] private CanvasGroup _canvasGroup;

    private UsableFlare _flare;

    private void Start() // Start Its Necessarily Part Not Awake
    {
        _canvasGroup.alpha = 0;

        _flare = Player.Instance.Flare;

        _slider.maxValue = _flare.UseDelay;
    }

    private void Update()
    {
        _slider.value = _flare.UseDelayCharge;

        if (_flare.UseDelayCharge > 0)
            _canvasGroup.alpha = 1;
        else
            Mathf.Clamp01(_canvasGroup.alpha -= Time.deltaTime * 2);

        if (_flare.CurrentUseCooldown > 0)
            _slider.value = _slider.maxValue;  // Just For Visual Effect

    }
}

