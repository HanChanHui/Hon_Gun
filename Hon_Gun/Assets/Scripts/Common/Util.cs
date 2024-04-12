using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Util
{

    /// <summary>
    /// ������Ʈ�� T Ÿ�� ���۳�Ʈ Ȯ�� ���� üũ �� ��ȯ Ȥ�� �߰�.
    /// </summary>
    public static T GetOrAddComponent<T>(GameObject _obj) where T : UnityEngine.Component
    {
        T c = _obj.GetComponent<T>();

        if(c == null)
        {
            c = _obj.AddComponent<T>();
        }
        return c;
    }

    /// <summary>
    /// ������Ʈ�� �ڽĿ�����Ʈ �� Ư�� �̸��� ���� ������Ʈ ��ȯ.
    /// </summary>
    public static GameObject FindChild(GameObject _obj, string _name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(_obj, _name, recursive);
        if(transform == null)
        {
            return null;
        }

        return transform.gameObject;
    }

    /// <summary>
    /// ������Ʈ�� �ڽĿ�����Ʈ �� Ư�� �̸��� ���� ������Ʈ Ž��.
    /// </summary>
    public static T FindChild<T>(GameObject _obj, string _name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if(_obj == null)
        {
            return null;
        }

        if(!recursive)
        {
            for(int i = 0; i < _obj.transform.childCount; i++)
            {
                Transform transform = _obj.transform.GetChild(i);
                if(string.IsNullOrEmpty(_name) || transform.name == _name)
                {
                    T component = transform.GetComponent<T>();
                    if(component != null)
                    {
                        return component;
                    }
                }
            }
        }
        else
        {
            foreach(T component in _obj.GetComponentsInChildren<T>(true))
            {
                if(string.IsNullOrEmpty(_name) || component.name == _name)
                {
                    return component;
                }
            }
        }
        return null;
    }



}
