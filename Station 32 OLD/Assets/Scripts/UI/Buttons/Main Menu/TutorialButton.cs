using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    public void OnTutorialButtonClick()
    {
        SceneManager.LoadScene(SceneList.Tutorial);
    }
}
