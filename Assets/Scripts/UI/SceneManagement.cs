using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement instance = null;

    [Header("세팅 패널"), SerializeField]
    GameObject _settingPanel;
    [Header("로딩 패널"), SerializeField]
    GameObject _loadingPanel;
    [Header("로딩 바"), SerializeField]
    Scrollbar _loadingBar;
    [Header("파괴 안되는 캔버스"), SerializeField]
    GameObject _staticCanvas;

    public bool _isLoad;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static SceneManagement Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(_staticCanvas);
    }
    public void GotoTitleScene()
    {
        StartCoroutine(LoadScene("Title"));
        _loadingPanel.SetActive(true);
        _isLoad = false;
    }

    public void GotoGameScene(bool isLoad)
    {
        Time.timeScale = 1;
        StartCoroutine(LoadScene("Game"));
        _loadingPanel.SetActive(true);
        _isLoad = isLoad;
    }
    IEnumerator LoadScene(string sceneName)
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                _loadingBar.size = Mathf.Lerp(_loadingBar.size, op.progress, timer);
                if (_loadingBar.size >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                _loadingBar.size = Mathf.Lerp(_loadingBar.size, 1f, timer);
                if (_loadingBar.size == 1.0f)
                {
                    op.allowSceneActivation = true;
                    _loadingPanel.SetActive(false);
                    yield break;
                }
            }
        }
    }
}
