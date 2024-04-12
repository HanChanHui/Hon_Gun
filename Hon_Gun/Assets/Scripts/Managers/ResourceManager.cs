using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    /// <summary>
    /// 프리팹이 Pool에 있으면 가져오고,
    /// 없으면 다시 호출.
    /// </summary>
    public T Load<T>(string _path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = _path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
            {
                name = name.Substring(index + 1);
            }

            GameObject go = Managers.MemoryPool.GetOriginal(name);
            if(go != null)
            {
                return go as T;
            }
        }

        return Resources.Load<T>(_path);
    }

    /// <summary>
    /// Load 함수를 통해 받은 프리팹을 통해 오브젝트를 생성.
    /// </summary>
    public GameObject Instantiate(string _path, Transform _parent = null)
    {
        GameObject original = Load<GameObject>($"{_path}");

        if(original == null)
        {
            Debug.Log($"Faild to load Resource : {_path}");
            return null;
        }

        if(original.GetComponent<MemoryPoolable>() != null)
        {
            return Managers.MemoryPool.Pop(original, _parent).gameObject;
        }

        GameObject go = Object.Instantiate(original, _parent);
        go.name = original.name;

        return go;
    }

    /// <summary>
    /// poolable 있을 경우 재사용을 위해 비활성화 후 Push.
    /// poolable 없을 경우 바로 삭제.
    /// </summary>
    public void Destroy(GameObject _obj, float _time = 0)
    {
        if(_obj == null)
        {
            return;
        }

        MemoryPoolable poolable = _obj.GetComponent<MemoryPoolable>();
        if(poolable != null)
        {
            Managers.MemoryPool.Push(poolable, _time);
            return;
        }

        Object.Destroy(_obj, _time);
    }
}
