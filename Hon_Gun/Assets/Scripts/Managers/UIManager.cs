using Consts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager
{

    private int _order = 10;

    /// <summary>
    /// ������Ʈ�� �˾� ĵ������ enum�� Ȱ���� Ȯ���� �� �ִ� Ű �����Ͽ� ����
    /// </summary>
    private Dictionary<PopupUIGroup, Stack<UI_Popup>> _popupStackDict = new Dictionary<PopupUIGroup, Stack<UI_Popup>>();
    /// <summary>
    /// ���� ĵ���� UI
    /// </summary>
    private UI_Scene sceneUI;

    /// <summary>
    /// UI ����/�����ϴ� ���� ����
    /// </summary>
    public GameObject Root()
    {
        GameObject root = GameObject.Find("UI_Root");
        if(root == null)
        {
            root = new GameObject { name = "UI_Root" };
        }

        return root;
    }

    /// <summary>
    /// ������Ʈ�� ĵ������ ������ sort order �� ����
    /// </summary>
    public void SetCanvas(GameObject _go, bool _sorting = false)
    {
        Canvas canvas = _go.GetOrAddComponent<Canvas>();
        Camera camera = GameObject.FindObjectOfType<Camera>();
        Debug.Log(camera);
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = camera;
        canvas.overrideSorting = true;

        if (_sorting)
        {
            canvas.sortingOrder = _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    /// <summary>
    /// ���� UI ����
    /// </summary>
    public T ShowSceneUI<T>(string _name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(_name))
        {
            _name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{_name}");

        T sceneUI = go.GetOrAddComponent<T>();
        this.sceneUI = sceneUI;

        go.transform.SetParent(Root().transform);
        return sceneUI;
    }

    /// <summary>
    /// �˾� UI ����
    /// </summary>
    public T ShowPopupUI<T>(string _name = null, bool forEffect = false) where T : UI_Popup
    {
        if(string.IsNullOrEmpty(_name))
        {
            _name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{_name}");

        go.transform.SetParent(Root().transform);

        T popup = go.GetOrAddComponent<T>();
        PopupUIGroup popupType = popup.PopupID;

        if(!_popupStackDict.ContainsKey(popupType))
        {
            _popupStackDict.Add(popupType, new Stack<UI_Popup>());
        }

        _popupStackDict[popupType].Push(popup);

        if(forEffect)
        {
            popup.GetComponent<Canvas>().sortingOrder = -1;
        }

        return popup as T;
    }

    /// <summary>
    /// �˾� UI ����
    /// </summary>
    public void ClosePopupUI(UI_Popup popup)
    {
        PopupUIGroup popupType = popup.PopupID;

        if (!_popupStackDict.TryGetValue(popupType, out Stack<UI_Popup> popupStack)
            || _popupStackDict[popupType].Count == 0)
        {
            return;
        }

        if (popup != popupStack.Peek())
        {
            Debug.Log("Close Popup Failed");
            return;
        }

        ClosePopupUI(popupType);
    }

    /// <summary>
    /// Enum�� ���� Value���� ���� UI ����
    /// �˾��� ������ ������
    /// </summary>
    public void ClosePopupUI(PopupUIGroup popupType)
    {
        if (!_popupStackDict.TryGetValue(popupType, out Stack<UI_Popup> popupStack)
            || _popupStackDict[popupType].Count == 0)
        {
            return;
        }

        UI_Popup popup = _popupStackDict[popupType].Pop();
        Managers.Resource.Destroy(popup.gameObject);
        _order--;

        popup = null;

        CheckPopupUICountAndRemove();
    }

    /// <summary>
    /// ��ϵ� ��� �˾� UI ����.
    /// </summary>
    public void CloseAllPopupUI()
    {
        foreach (KeyValuePair<PopupUIGroup, Stack<UI_Popup>> kv in _popupStackDict)
        {
            PopupUIGroup popupType = kv.Key;
            Stack<UI_Popup> popupStack = kv.Value;

            while (popupStack.Count != 0)
            {
                UI_Popup popup = popupStack.Pop();
                Managers.Resource.Destroy(popup.gameObject);
                _order--;
                popup = null;
            }
        }
        CheckPopupUICountAndRemove();
    }

    /// <summary>
    /// ������ �˾� UI �׷쿡 ���� ��� �˾� ����.
    /// </summary>
    public void CloseAllGroupPopupUI(PopupUIGroup popupType)
    {
        if (!_popupStackDict.TryGetValue(popupType, out Stack<UI_Popup> popupStack)
            || _popupStackDict[popupType].Count == 0)
        {
            return;
        }

        while (popupStack.Count != 0)
        {
            UI_Popup popup = popupStack.Pop();
            Managers.Resource.Destroy(popup.gameObject);
            _order--;
            popup = null;
        }

        CheckPopupUICountAndRemove();
    }

    /// <summary>
    /// �˾� UI���� ����Ʈ�� ��ȸ ��.
    /// Stack�� �ִ� UI 0���̸� üũ �� ����.
    /// </summary>
    private void CheckPopupUICountAndRemove()
    {
        List<PopupUIGroup> popupType = new List<PopupUIGroup>();

        foreach (PopupUIGroup popupUI in _popupStackDict.Keys)
        {
            popupType.Add(popupUI);
        }

        for (int i = 0; i < _popupStackDict.Count; i++)
        {
            if (_popupStackDict.GetValueOrDefault<PopupUIGroup, Stack<UI_Popup>>(popupType[i]).Count == 0)
            {
                _popupStackDict.Remove(popupType[i]);
            }
        }
        CheckPopupUICountInScene();
    }

    /// <summary>
    /// �˾� ������ � �ִ� Ȯ��.
    /// </summary>
    private void CheckPopupUICountInScene()
    {
        Debug.Log($"popupCount : {_popupStackDict.Count}");

        foreach (PopupUIGroup popupKey in _popupStackDict.Keys)
        {
            Debug.Log($"popupList : {popupKey}");
        }

        if (_popupStackDict.Count == 0)
        {
            return;
        }
    }

    public void UIClear()
    {
        CloseAllPopupUI();
        sceneUI = null;
    }

}
