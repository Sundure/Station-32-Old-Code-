using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    [SerializeField] private GameObject _loadScreen;
    public void OnTutorialButtonClick()
    {
        _loadScreen.SetActive(true);
        SceneManager.LoadScene(SceneList.Tutorial);
    }
}
