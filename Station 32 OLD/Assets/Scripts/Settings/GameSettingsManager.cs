using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
    /// <summary>
    /// If You Want To Get GameSettings On Scene Create Moment, Use Start
    /// </summary>
    [HideInInspector] public GameSettings GameSettings;

    public static GameSettingsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        GameSettings = LoadSettings();
    }

    public void SaveSettings(GameSettings settings)
    {
        string json = JsonUtility.ToJson(settings);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/GameSettings.json", json);
    }

    public GameSettings LoadSettings()
    {
        string path = Application.persistentDataPath + "/GameSettings.json";

        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            return JsonUtility.FromJson<GameSettings>(json);
        }

        return new();
    }
}
