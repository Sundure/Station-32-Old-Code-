using UnityEngine;
using UnityEngine.UI;

public class LoadingPanelManager : MonoBehaviour
{
    private Slider _slider;

    [SerializeField] private GameObject _loadingPanelPrefab;

    private static LoadingPanelManager _instance;

    private AsyncOperation _asyncOperation;

    private Transform _canvasTransform;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }

        _instance = this;

        DontDestroyOnLoad(gameObject);

        FindNewCanvasTransform();

        SceneManager.OnSceneStartLoading += SpawnLoadingPanel;
        SceneManager.OnSceneLoaded += OnSceneLoaded;

        enabled = false;
    }

    private void Update()
    {
        _slider.value = _asyncOperation.progress;
    }

    private void SpawnLoadingPanel(AsyncOperation asyncOperation)
    {
        if (_canvasTransform == null)
            return;

        GameObject panel = Instantiate(_loadingPanelPrefab, _canvasTransform);

        if (panel.TryGetComponent(out _slider))
        {
            _asyncOperation = asyncOperation;

            enabled = true;
        }
    }

    private void OnSceneLoaded()
    {
        enabled = false;
        FindNewCanvasTransform();
    }

    private void FindNewCanvasTransform()
    {
        _canvasTransform = FindFirstObjectByType<Canvas>().transform;
    }

    private void OnDestroy()
    {
        SceneManager.OnSceneStartLoading -= SpawnLoadingPanel;
        SceneManager.OnSceneLoaded -= OnSceneLoaded;
    }
}
