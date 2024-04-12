using Consts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public CanvasGroup canvasGroup;
    private string _loadSceneName;

    private static SceneManagerEX Create()
    {
        Debug.Log("¤¾¤·");
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
        yield return StartCoroutine(Fade(true));

        AsyncOperation op = SceneManager.LoadSceneAsync(_loadSceneName);
        op.allowSceneActivation = false;

        while(!op.isDone)
        {
            yield return null;
            if(op.progress > 0.9f)
            {

            }
            else
            {
                op.allowSceneActivation = true;
                yield break;
            }
        }
    }


    public void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == _loadSceneName)
        {
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    IEnumerator Fade(bool isFadeIn)
    {
        if(!isFadeIn)
        {
            yield return new WaitForSeconds(0.2f);
        }

        float timer = 0f;
        while(timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 2f;
            canvasGroup.alpha = isFadeIn ? Mathf.Lerp(0f, 1f, timer) : Mathf.Lerp(1f, 0f, timer);
        }

        if(!isFadeIn)
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
