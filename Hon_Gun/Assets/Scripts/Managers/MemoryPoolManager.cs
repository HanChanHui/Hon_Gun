using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPoolManager
{ 

    #region MemoryPooling
    class MemoryPooling
    {
        public GameObject original { get; private set; }
        public Transform root { get; set; }
        /// <summary>
        /// MemoryPoolable 스크리브 여부를 통해 풀링 할 오브젝트인지 구분.
        /// 오브젝트를 스택으로 관리.
        /// </summary>
        Stack<MemoryPoolable> poolStack = new Stack<MemoryPoolable>();

        /// <summary>
        /// 풀링 초기화.
        /// </summary>
        public void Init(GameObject _original, int _count = 5)
        {
            original = _original;
            root = new GameObject().transform;
            root.name = $"{original.name}_Root";
            for (int i = 0; i < _count; i++)
            {
                Push(Create());
            }
        }
        /// <summary>
        /// 풀링에 사용할 오브젝트 생성.
        /// </summary>
        private MemoryPoolable Create()
        {
            GameObject go = Object.Instantiate(original);
            go.name = original.name;
            return go.GetOrAddComponent<MemoryPoolable>();
        }
        /// <summary>
        /// 오브젝트 풀에 넣기.(오브젝트 비활성화 상태)
        /// </summary>
        public void Push(MemoryPoolable _poolable)
        {
            if (_poolable == null)
            {
                return;
            }

            _poolable.transform.parent = root;
            _poolable.gameObject.SetActive(false);
            _poolable.isUsing = false;

            poolStack.Push(_poolable);
        }
        /// <summary>
        /// 오브젝트 풀로부터 꺼내오기.(오브젝트 활성화 상태)
        /// </summary>
        public MemoryPoolable Pop(Transform parent)
        {
            MemoryPoolable poolable = null;
            while (poolStack.Count > 0)
            {
                poolable = poolStack.Pop();
                if (!poolable || !poolable.gameObject.activeSelf)
                {
                    break;
                }
            }
            if (poolable == null || poolable.gameObject.activeSelf)
            {
                poolable = Create();
            }
            poolable.gameObject.SetActive(true);
            if (parent == null)
            {
                ///poolable.transform.parent = parent;
            }
            poolable.isUsing = true;
            return poolable;
        }
    }

    #endregion

    private Dictionary<string, MemoryPooling> pool = new Dictionary<string, MemoryPooling>();
    private Transform _root;

    public void Init()
    {
        if (_root == null)
        {
            _root = new GameObject { name = "Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    /// <summary>
    /// Key와 일치하는 해당 풀에 해당 오브젝트 poolable을 Push 함수에 호출해 추가.
    /// </summary>
    public void Push(MemoryPoolable _poolable, float _time)
    {
        string name = _poolable.gameObject.name;
        if (!pool.ContainsKey(name))
        {
            Object.Destroy(_poolable.gameObject, _time);
            return;
        }
        pool[name].Push(_poolable);
    }

    /// <summary>
    /// Dictionary에 보관 중인 프리팹 이름에 해당하는 Key의 Value인 풀을 리턴.
    /// </summary>
    public MemoryPoolable Pop(GameObject _original, Transform parent = null)
    {
        if (!pool.ContainsKey(_original.name))
        {
            CreatePool(_original);
        }

        return pool[_original.name].Pop(parent);
    }

    /// <summary>
    /// pool을 생성 및 Dictionary에 추가.
    /// </summary>
    public void CreatePool(GameObject _original, int _count = 100)
    {
        MemoryPooling pool = new MemoryPooling();
        pool.Init(_original, _count);
        pool.root.parent = _root;

        this.pool.Add(_original.name, pool);
    }

    /// <summary>
    /// Dictionary을 통해 Pool Value의 original에 원본 프리팹을 리턴.
    /// </summary>
    public GameObject GetOriginal(string _name)
    {
        if (!pool.ContainsKey(_name))
            return null;
        return pool[_name].original;
    }

    /// <summary>
    /// 모든 pool을 제거하여 초기화.
    /// </summary>
    public void Clear()
    {
        foreach (Transform child in _root)
        {
            Object.Destroy(child.gameObject);
        }
        pool.Clear();
    }

}
