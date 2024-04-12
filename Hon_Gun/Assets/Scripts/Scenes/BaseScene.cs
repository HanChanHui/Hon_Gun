using Consts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
   public abstract ScenesType scenesType { get; }


    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));

        if(obj == null)
        {
            Managers.Resource.Instantiate("UI/Scene").name = "@Scene";
        }
    }

    public abstract void Clear();
}
