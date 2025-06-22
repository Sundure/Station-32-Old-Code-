using TMPro;
using UnityEngine;

public class ItemsUI : MonoBehaviour
{
    public static ItemsUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _fuelCountText;
    [SerializeField] private TextMeshProUGUI _flareCountText;

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

        _fuelCountText.text = Player.Instance.FuelCount.ToString();
        _flareCountText.text = Player.Instance.FlareCount.ToString();
    }

    public void ChangeFuelCount()
    {
        _fuelCountText.text = Player.Instance.FuelCount.ToString();
    }
    public void ChangeFlareCount()
    {
        _flareCountText.text = Player.Instance.FlareCount.ToString();
    }
}
