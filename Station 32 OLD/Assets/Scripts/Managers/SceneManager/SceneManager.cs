using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;

    public readonly static List<Scene> Scenes = new();

    public static event Action OnSceneLoaded;
    public static event Action<AsyncOperation> OnSceneStartLoading;
    public static event Action<SceneList> OnSceneFromListLoaded;

    public static List<Scene> ScenesWithFixedTimeScale { get; private set; } = new();

    private const string MAIN_MENU_SCENE = "MainMenu";

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

        UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneLoaded;

        DontDestroyOnLoad(gameObject);

        int listScenesCount = Enum.GetValues(typeof(SceneList)).Length;
        int buildScenesCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;

        string[] buildScenesNames = new string[buildScenesCount];

        for (int i = 0; i < buildScenesCount; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            buildScenesNames[i] = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        }

        if (buildScenesCount != listScenesCount)
        {
            Debug.LogError($"\"SceneList\" Scenes Count ({listScenesCount}) Does Not Equal \"Build Profile\" Scenes Count ({buildScenesCount}) ");
        }

        for (int i = 0; i < buildScenesCount; i++)
        {
            for (int j = 0; j < listScenesCount; j++)
            {
                string listSceneName = ((SceneList)j).ToString();

                if (buildScenesNames[i] == listSceneName)
                {
                    break;
                }
                else if (j == listScenesCount - 1)
                {
                    Debug.LogError($"Scene \"{buildScenesNames[i]}\" From \"Build Profile\" Does Not Exist In \"SceneList\"");
                }
            }
        }

        for (int i = 0; i < listScenesCount; i++)
        {
            string listSceneName = ((SceneList)i).ToString();

            for (int j = 0; j < buildScenesCount; j++)
            {
                if (buildScenesNames[j] == listSceneName)
                {
                    break;
                }
                else if (j == buildScenesCount - 1)
                {
                    Debug.LogError($"Scene \"{listSceneName}\" From \"Build Profile\" Does Not Exist In \"SceneList\"");
                }
            }
        }
    }

    private void SceneLoaded(Scene scene, LoadSceneMode _)
    {
        if (scene.name == MAIN_MENU_SCENE)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        Debug.Log($"Loaded Scene {scene.name}");

        OnSceneLoaded?.Invoke();

        if (Enum.TryParse(scene.name, out SceneList sceneList))
        {
            OnSceneFromListLoaded?.Invoke(sceneList);
        }
    }

    public static void LoadScene(SceneList scene)
    {
        OnSceneStartLoading?.Invoke(UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene.ToString()));
    }

    private void OnDestroy()
    {
        Debug.Log($"{this} Destroyed");

        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= SceneLoaded;
    }
}
