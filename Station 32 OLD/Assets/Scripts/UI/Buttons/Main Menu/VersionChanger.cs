using TMPro;
using UnityEngine;

public class VersionChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        _text.text = "V." + Application.version;
    }
}
