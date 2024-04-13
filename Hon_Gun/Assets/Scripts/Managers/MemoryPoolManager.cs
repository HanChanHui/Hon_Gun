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
        /// MemoryPoolable ��ũ���� ���θ� ���� Ǯ�� �� ������Ʈ���� ����.
        /// ������Ʈ�� �������� ����.
        /// </summary>
        Stack<MemoryPoolable> poolStack = new Stack<MemoryPoolable>();

        /// <summary>
        /// Ǯ�� �ʱ�ȭ.
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
        /// Ǯ���� ����� ������Ʈ ����.
        /// </summary>
        private MemoryPoolable Create()
        {
            GameObject go = Object.Instantiate(original);
            go.name = original.name;
            return go.GetOrAddComponent<MemoryPoolable>();
        }
        /// <summary>
        /// ������Ʈ Ǯ�� �ֱ�.(������Ʈ ��Ȱ��ȭ ����)
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
        /// ������Ʈ Ǯ�κ��� ��������.(������Ʈ Ȱ��ȭ ����)
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
    /// Key�� ��ġ�ϴ� �ش� Ǯ�� �ش� ������Ʈ poolable�� Push �Լ��� ȣ���� �߰�.
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
    /// Dictionary�� ���� ���� ������ �̸��� �ش��ϴ� Key�� Value�� Ǯ�� ����.
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
    /// pool�� ���� �� Dictionary�� �߰�.
    /// </summary>
    public void CreatePool(GameObject _original, int _count = 100)
    {
        MemoryPooling pool = new MemoryPooling();
        pool.Init(_original, _count);
        pool.root.parent = _root;

        this.pool.Add(_original.name, pool);
    }

    /// <summary>
    /// Dictionary�� ���� Pool Value�� original�� ���� �������� ����.
    /// </summary>
    public GameObject GetOriginal(string _name)
    {
        if (!pool.ContainsKey(_name))
            return null;
        return pool[_name].original;
    }

    /// <summary>
    /// ��� pool�� �����Ͽ� �ʱ�ȭ.
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
