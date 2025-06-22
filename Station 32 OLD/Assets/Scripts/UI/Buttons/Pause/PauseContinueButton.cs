using UnityEngine;

public class PauseContinueButton : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;

    public void Continue()
    {
        PauseManager.ChangePause(false);

        PausePanel.SetActive(false);
    }
}
