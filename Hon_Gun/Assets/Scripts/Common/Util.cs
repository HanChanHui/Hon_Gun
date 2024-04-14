using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Util
{

    /// <summary>
    /// 오브젝트에 T 타입 컴퍼넌트 확인 여부 체크 후 반환 혹은 추가.
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
    /// 오브젝트의 자식오브젝트 중 특정 이름을 가진 오브젝트 반환.
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
    /// 오브젝트의 자식오브젝트 중 특정 이름을 가진 컴포넌트 탐색.
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

    /// <summary>
    /// 원하는 Layer가 있는지 확인 후, 인덱스 반환.
    /// </summary>
    public static int MaskToLayer(LayerMask mask)
    {
        var bitmask = mask.value;

        UnityEngine.Assertions.Assert.IsFalse((bitmask & (bitmask - 1)) != 0,
            "MaskToLayer() was passed an invalid mask containing multiple layers.");

        int result = bitmask > 0 ? 0 : 31;
        while(bitmask > 1)
        {
            bitmask = bitmask >> 1;
            result++;
        }
        return result;
    }


}
