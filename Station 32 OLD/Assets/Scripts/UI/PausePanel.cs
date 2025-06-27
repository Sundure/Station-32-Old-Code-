using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;

    public DirectoryManager DirectoryManager { get; private set; }

    [SerializeField] private Directory _defaultDirectory;

    public static PausePanel Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DirectoryManager = new();
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseManager.Pause == true)
            {
                if (_pausePanel.activeSelf)
                {
                    DirectoryManager.UndoDirectory();
                    return;
                }

                SwitchPausePanel(true);
                PauseManager.Instance.enabled = false;
                return;
            }
        }
    }

    public void SwitchPausePanel(bool enabled)
    {
        if (enabled == _pausePanel.activeSelf)
            return;

        _pausePanel.SetActive(enabled);

        if (enabled)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            DirectoryManager.CurrentDirectory = _defaultDirectory; // Set Default Directory When Pause Panel Enabled
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void UndoDirectory() // This Function Are Made For Buttons OnClick Events
    {
        DirectoryManager.UndoDirectory();
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
