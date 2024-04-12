using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public static class Extension
{
    public static void AddUIEvent(this GameObject go, Action<PointerEventData> action)
    {
        UI_Base.BindUIEvent(go, action);
    }

    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        return Util.GetOrAddComponent<T>(go);
    }
}
