using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadManager : SingletonMonobehaviour<SceneLoadManager>
{
    public event System.EventHandler OnPurge;

    [SerializeField] private CanvasGroup loadingScreenCanvasGroup;
    [SerializeField] private Image loadingScreenImage;

    private readonly float loadingScreenDuration = 0.75f;
    private bool isLoadingScreenActive;

    //===========================================================================
    protected override void Awake()
    {
        base.Awake();

        Time.timeScale = 1.0f;
    }

    private IEnumerator Start()
    {
        MainMenuUI.Instance.SetActive(true);

        loadingScreenImage.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        loadingScreenCanvasGroup.alpha = 1.0f;

        yield return StartCoroutine(LoadingScreen(0.0f));
    }

    //===========================================================================
    private IEnumerator UnloadAndSwitchScene(string sceneName)
    {
        EventManager.CallBeforeLoadingScreenEvent();
        yield return StartCoroutine(LoadingScreen(1.0f));

        EventManager.CallBeforeUnloadingSceneEvent();
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));
        EventManager.CallAfterSceneLoadEvent();

        yield return StartCoroutine(LoadingScreen(0.0f));
        EventManager.CallAfterLoadingScreenEvent();
    }

    private IEnumerator LoadingScreen(float targetAlpha)
    {
        isLoadingScreenActive = true;

        loadingScreenCanvasGroup.blocksRaycasts = true;

        float _loadSpeed = Mathf.Abs(loadingScreenCanvasGroup.alpha - targetAlpha) / loadingScreenDuration;

        while (Mathf.Approximately(loadingScreenCanvasGroup.alpha, targetAlpha) == false)
        {
            loadingScreenCanvasGroup.alpha = Mathf.MoveTowards(loadingScreenCanvasGroup.alpha, targetAlpha, _loadSpeed * Time.deltaTime);
            yield return null;
        }

        isLoadingScreenActive = false;

        loadingScreenCanvasGroup.alpha = targetAlpha;
        loadingScreenCanvasGroup.blocksRaycasts = false;
    }

    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

        SceneManager.SetActiveScene(newlyLoadedScene);
    }

    private IEnumerator LoadStartingScene()
    {
        EventManager.CallBeforeLoadingScreenEvent();
        yield return StartCoroutine(LoadingScreen(1.0f));

        OnPurge?.Invoke(this, System.EventArgs.Empty);

        yield return StartCoroutine(LoadingScreen(0.0f));
        EventManager.CallAfterLoadingScreenEvent();
    }

    //===========================================================================
    public void LoadScene(string sceneName)
    {
        if (isLoadingScreenActive == false)
        {
            StartCoroutine(UnloadAndSwitchScene(sceneName));
        }
    }

    public void LoadTestingMap()
    {
        if (isLoadingScreenActive == false)
        {
            StartCoroutine(LoadStartingScene());
        }
    }
}