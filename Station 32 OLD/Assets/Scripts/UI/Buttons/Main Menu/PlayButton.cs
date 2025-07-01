using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public void SceneChanger()
    {
        SceneManager.LoadScene(SceneList.Station32);
    }
}
