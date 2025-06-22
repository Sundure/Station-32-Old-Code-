using UnityEngine;

public class PauseExitButton : MonoBehaviour
{

    public void Exit()
    {
        PauseManager.ChangePause(false);

        SceneManager.LoadScene(SceneList.MainMenu);
    }
}
