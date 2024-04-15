using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Consts;

public abstract class UI_Base : MonoBehaviour
{
    public abstract void Init();

    private void Awake()
    {
        Init();
    }

    Dictionary<Type, UnityEngine.Object[]> objects = new Dictionary<Type, UnityEngine.Object[]>();

    protected void Bind<T> (Type _type) where T : UnityEngine.Object
    {
        string[] name = Enum.GetNames(_type);

        if(this.objects.ContainsKey(typeof(T)))
        {
            return;
        }

        UnityEngine.Object[] objects = new UnityEngine.Object[name.Length];

        this.objects.Add(typeof(T), objects);

        for(int i = 0; i < name.Length; ++i)
        {
            if(typeof(T) == typeof(GameObject))
            {
                objects[i] = Util.FindChild(gameObject, name[i], true);
            }
            else
            {
                objects[i] = Util.FindChild<T>(gameObject, name[i], true);
            }

            if (objects[i] == null)
            {
                Debug.Log($"Fail to Bind {name[i]}!");
            }
        }
    }

    protected T Get<T>(int _idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        this.objects.TryGetValue(typeof(T), out objects);

        return objects[_idx] as T;
    }

    protected GameObject GetObject(int _idx) { return Get<GameObject>(_idx);  }
    protected Image GetImage(int _idx) { return Get<Image>(_idx); }
    protected Text GetText(int _idx) { return Get<Text>(_idx); }
    protected Button GetButton(int _idx) { return Get<Button>(_idx); }
    protected Slider GetSlider(int _idx) { return Get<Slider>(_idx); }

    public static void BindUIEvent(GameObject _go, Action<PointerEventData> _action)
    {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(_go);

        evt.OnClickHandler -= _action;
        evt.OnClickHandler += _action;
    }

}
