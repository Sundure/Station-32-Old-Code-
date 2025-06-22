using UnityEngine;
using UnityEngine.UI;

public class VersionChanger : MonoBehaviour
{
    [SerializeField] private Text _versionControl;
    void Start()
    {
        string version = Application.version;
        _versionControl.text = "V." + version;
    }
}
