using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowManager : MonoBehaviour
{
    [SerializeField] private SceneCatalog _catalog;
    [SerializeField] private SceneTransitionUI _transitionUI;
    [SerializeField] private float _fadeDuration = 0.7f;

    public static SceneFlowManager Instance { get; private set; }
    private bool _isLoading = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (_catalog == null)
        {
            Debug.LogWarning("카탈로그 비었음");
            Destroy(gameObject);
            return;
        }

        _catalog.BuildMaps();
    }

    void Start()
    {
        if (_transitionUI != null)
            _transitionUI.Init();

        InitScene();
    }

    void InitScene()
    {
        string current = SceneManager.GetActiveScene().name;

        if (string.IsNullOrEmpty(current))
            return;
        GetSceneBGMType(current, out EBgmType type);

        SoundManager.Instance.PlayBGM(type);
    }

    public void LoadScene(ESceneID id)
    {
        SoundManager.Instance.StopBGM();
        if (_catalog.TryGetSceneName(id, out string sceneName) == false)
            return;
        if (string.IsNullOrEmpty(sceneName))
            return;

        StartCoroutine(Co_LoadSceneWithTransition(id, sceneName));
    }

    void GetSceneBGMType(string sceneName, out EBgmType type)
    {
        type = sceneName switch
        {
            nameof(EBgmType.Lobby) => EBgmType.Lobby,
            nameof(EBgmType.Stage) => EBgmType.Stage,
            _ => EBgmType.Lobby
        };
    }

    IEnumerator Co_LoadSceneWithTransition(ESceneID id, string sceneName)
    {
        if (_isLoading)
        {
            Debug.Log("씬 전환중 입력 무시");
            yield break;
        }

        _isLoading = true;

        if (_transitionUI != null)
        {
            yield return _transitionUI.CO_FadeTo(1f, _fadeDuration);
        }

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        op.allowSceneActivation = false;

        while (op.progress < 0.9f)
            yield return null;

        op.allowSceneActivation = true;

        yield return null;

        if (_transitionUI != null)
        {
            yield return _transitionUI.CO_FadeTo(0f, _fadeDuration);
        }

        if (GameManager.Instance != null)
        {
            Debug.Log("게임매니저있음 = 스테이지 내부");
            GameManager.Instance.PauseGame(false);
        }

        GetSceneBGMType(sceneName, out EBgmType type);
        SoundManager.Instance.PlayBGM(type);

        Debug.Log($"씬 로드 -> {sceneName}");
        _isLoading = false;
    }

    public void ReloadScene()
    {
        string current = SceneManager.GetActiveScene().name;

        if (_catalog.TryGetSceneId(current, out ESceneID id) == false)
        {
            Debug.LogWarning($"리로드 실패 -> {current}가 카탈로그에 없음");
            return;
        }

        Debug.Log($"재시작 : {current}");

        LoadScene(id);
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
