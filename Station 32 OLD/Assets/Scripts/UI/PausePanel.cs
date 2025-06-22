using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;

    private void Awake()
    {
        PauseManager.OnPauseChange += SwitchPausePanel;
    }

    private void SwitchPausePanel(bool enabled)
    {
        if (enabled == _pausePanel.activeSelf)
            return;

        _pausePanel.SetActive(enabled);

        if (enabled)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void OnDestroy()
    {
        PauseManager.OnPauseChange -= SwitchPausePanel;
    }
}
