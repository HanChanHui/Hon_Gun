using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerEX : MonoBehaviour
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    private static SceneManagerEX instance;

    public static SceneManagerEX Instance
    {
        get
        {
            if(instance == null)
            {
                var obj = FindObjectOfType<SceneManagerEX>();
                if(obj != null)
                {
                    instance = obj;
                }
                else
                {
                    instance = Create();
                }
            }

            return instance;
        }
    }
    [SerializeField]
    public CanvasGroup barCanvasGroup;
    [SerializeField]
    public CanvasGroup fadeCanvasGroup;
    [SerializeField]
    private Image progressBar;


    private string _loadSceneName;

    private static SceneManagerEX Create()
    {
        return Instantiate(Resources.Load<SceneManagerEX>("UI/UI_Loading"));
    }

    private void Awake()
    {
        if(Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(ScenesType type)
    {
        //Create();
        gameObject.SetActive(true);
        SceneManager.sceneLoaded += OnSceneLoaded;
        _loadSceneName = GetSceneName(type);
        StartCoroutine(LoadSceneProcess());

        CurrentScene.Clear();
    }

    IEnumerator LoadSceneProcess()
    {
        progressBar.fillAmount = 0f;
        yield return StartCoroutine(Fade(fadeCanvasGroup, true, 2f));
         
        AsyncOperation op = SceneManager.LoadSceneAsync(_loadSceneName);
        op.allowSceneActivation = false;
        StartCoroutine(Fade(barCanvasGroup, true, 2f));

        float timer = 0f;
        while(!op.isDone)
        {
            yield return null;
            if(op.progress > 0.9f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                
                if (progressBar.fillAmount >= 1f)
                {
                    
                    yield return new WaitForSeconds(0.3f);
                    op.allowSceneActivation = true;
                    
                    yield break;
                }
            }
        }
    }


    public void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == _loadSceneName)
        {
            barCanvasGroup.alpha = 0f;
            StartCoroutine(Fade(fadeCanvasGroup, false, 2f));

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    IEnumerator Fade(CanvasGroup _cg, bool _isFadeIn, float _time)
    {
        if(!_isFadeIn)
        {
            //yield return new WaitForSeconds(0.2f);
        }

        float timer = 0f;
        while(timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * _time;
            _cg.alpha = _isFadeIn ? Mathf.Lerp(0f, 1f, timer) : Mathf.Lerp(1f, 0f, timer);
        }

        if(!_isFadeIn)
        {
            gameObject.SetActive(false);
        }
    }


    private string GetSceneName(ScenesType type)
    {
        string sceneName = System.Enum.GetName(typeof(ScenesType), type);
        return sceneName;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
